<template>
    <div class="container">

        <div class="row" v-if="formState === 'list'">
            <h1>Notes</h1>
            <table class="table table-striped table-ellipsis" aria-labelledby="tableLabel" v-if="notes">
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
                        <td class="text-truncate">{{ note.contents }}</td>
                        <td>{{ formatDate(note.created) }}</td>
                        <td>{{ formatDate(note.updated) }}</td>
                        <td><button @click="startUpdate(note)" class="btn btn-primary btn-sm mr-1">Update</button> <button @click="deleteNote(note.id)" class="btn btn-danger btn-sm">Delete</button></td>
                    </tr>
                </tbody>
            </table>
            <div><button id="add" @click="startAdd()" class="btn btn-primary btn-sm">Add New</button></div>
        </div>
        <div class="row" v-if="formState === 'new'">
            <div class="col-9">
                <h1>Add Note</h1>
                <form>
                    <div class="form-group">
                        <label for="editName">Name</label>
                        <input id="editName" type="text" class="form-control" v-model="noteToAdd.name" maxlength="50" />
                    </div>
                    <div class="form-group">
                        <label for="editContents">Contents</label>
                        <input id="editContents" type="text" class="form-control" v-model="noteToAdd.contents" />
                    </div>
                    <button id="addSave" @click="saveAdd()" class="btn btn-primary btn-sm mr-1">Save</button>
                    <button id="addCancel" @click="cancelAdd()" class="btn btn-primary btn-sm">Cancel</button>
                </form>
            </div>
        </div>
        <div class="row" v-if="formState === 'edit'">
            <div class="col-9">
                <h1>Edit Note</h1>
                <form>
                    <div class="form-group">
                        <label for="editName">Name</label>
                        <input id="editName" type="text" class="form-control" v-model="noteToUpdate.name"  maxlength="50"/>
                    </div>
                    <div class="form-group">
                        <label for="editContents">Contents</label>
                        <input id="editContents" type="text" class="form-control" v-model="noteToUpdate.contents" />
                    </div>
                    <button id="editSave" @click="saveUpdate()" class="btn btn-primary btn-sm mr-1">Save</button><button id="editCancel" @click="cancelUpdate()" class="btn btn-primary btn-sm">Cancel</button>
                </form>
            </div>
        </div>
    </div>
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
        created() {
            this.getNotes();
            this.reset(formStateConstant.list);
        }
    }
</script>

<style scoped>
    .table.table-ellipsis tbody td {
        max-width: 100px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap
    }
    th, td {
        text-align: left
    }
</style>
