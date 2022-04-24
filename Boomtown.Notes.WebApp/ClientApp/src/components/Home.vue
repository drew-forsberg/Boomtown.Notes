<template>
    <h1 id="tableLabel">Notes</h1>
    <template v-if="formState === 'list'">
        <div><button id="add" @click="startAdd()">Add</button></div>
        <table class='table table-striped' aria-labelledby="tableLabel" v-if="notes">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Contents</th>
                    <th>Created</th>
                    <th>Updated</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="note of notes" v-bind:key="note.id">
                    <td>{{ note.id }}</td>
                    <td>{{ note.name }}</td>
                    <td>{{ note.contents }}</td>
                    <td>{{ formatDate(note.created) }}</td>
                    <td>{{ formatDate(note.updated) }}</td>
                    <td><button @click="startUpdate(note)">Update</button></td>
                    <td><button @click="deleteNote(note.id)">Delete</button></td>
                </tr>
            </tbody>
        </table>
    </template>
    <template v-if="formState === 'new'">
        <fieldset>
            <div>
                <label for="editName">Name</label>
                <input id="editName" type="text" v-model="noteToAdd.name" />
            </div>
            <div>
                <label for="editContents">Contents</label>
                <input id="editContents" type="text" v-model="noteToAdd.contents" />
            </div>
        </fieldset>
        <div><button id="addSave" @click="saveAdd()">Save</button><button id="addCancel" @click="cancelAdd()">Cancel</button></div>
    </template>
    <template v-if="formState === 'edit'">
        <fieldset>
            <div>
                <label for="editName">Name</label>
                <input id="editName" type="text" v-model="noteToUpdate.name" />
            </div>
            <div>
                <label for="editContents">Contents</label>
                <input id="editContents" type="text" v-model="noteToUpdate.contents" />
            </div>
        </fieldset>
        <div><button id="editSave" @click="saveUpdate()">Save</button><button id="editCancel" @click="cancelUpdate()">Cancel</button></div>
    </template>
</template>

<script>
    import axios from 'axios'

    const formStateConstant = {
        list: 'list',
        edit: 'edit',
        new: 'new'
    };

    export default {
        name: "Home",
        data() {
            return {
                notes: [],
                noteToUpdate: undefined,
                noteToAdd: undefined,
                formState: formStateConstant.list
            }
        },
        methods: {
            formatDate(dateString) {
                if (!dateString) {
                    return '';
                }
                const date = new Date(dateString);
                return new Intl.DateTimeFormat('default', { dateStyle: 'short', timeStyle: 'short' }).format(date);
            },
            async getNotes() {

                try {
                    const response = await axios.get('api/notes');
                    this.notes = response.data;
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            async getNote(id) {

                try {
                    const response = await axios.get(`api/notes/${id}`);
                    this.note = response.data;
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            startAdd() {
                this.noteToAdd = {
                    name: '',
                    contents: ''
                };
                this.formState = formStateConstant.new;
            },
            cancelAdd() {
                this.reset(formStateConstant.list);
            },
            async saveAdd() {
                try {
                    await axios.post('api/notes', this.noteToAdd);
                    await this.getNotes();
                    this.reset(formStateConstant.list);
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            startUpdate(note) {
                this.noteToUpdate = note;
                this.formState = formStateConstant.edit;
            },
            cancelUpdate() {
                this.reset(formStateConstant.list);
            },
            async saveUpdate() {
                try {
                    await axios.put(`api/notes/${this.noteToUpdate.id}`, this.noteToUpdate);
                    await this.getNotes();
                    this.reset(formStateConstant.list);
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            async deleteNote(id) {

                try {
                    await axios.delete(`api/notes/${id}`);
                    await this.getNotes();
                    this.reset(formStateConstant.list);
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            reset(formState) {
                this.noteToUpdate = undefined;
                this.noteToAdd = undefined;
                this.formState = formState;
            }
        },
        mounted() {
            this.getNotes();
            this.reset(formStateConstant.list);
        }
    }
</script>

<style scoped>
    th, td {
        text-align: left
    }
</style>
