<template>
    <div class="row">
        <div class="col-9">
            <h1>Edit Note</h1>
            <fieldset>
                <div class="form-group">
                    <label for="editName">Name</label>
                    <input id="editName" type="text" class="form-control" v-model="note.name" maxlength="50" />
                </div>
                <div class="form-group">
                    <label for="editContents">Contents</label>
                    <input id="editContents" type="text" class="form-control" v-model="note.contents" />
                </div>
                <button id="editSave" @click="saveEdit()" class="btn btn-primary btn-sm mr-1">Save</button>
                <button id="editCancel" @click="cancelEdit()" class="btn btn-primary btn-sm">Cancel</button>
            </fieldset>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'

    export default {
        name: "Edit",
        data() {
            return {
                note: {
                    name: '',
                    contents: ''
                }
            }
        },
        props:
        {
            id: { required: true }
        },
        methods: {
            async getNote(id) {

                try {
                    const response = await axios.get(`/api/notes/${id}`);
                    this.note = response.data;
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            cancelEdit() {
                this.$router.push({ name: "home" });
            },
            async saveEdit() {
                try {
                    await axios.put(`/api/notes/${this.note.id}`, this.note);
                    this.$router.push({ name: "home" });
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            }
        },
        async created() {
            await this.getNote(this.id);
        }
    }
</script>
