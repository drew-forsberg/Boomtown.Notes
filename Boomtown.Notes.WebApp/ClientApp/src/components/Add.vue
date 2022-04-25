<template>
    <div class="row">
        <div class="col-9">
            <h1>Add Note</h1>
            <fieldset>
                <div class="form-group">
                    <label for="addName">Name</label>
                    <input id="addName" type="text" class="form-control" v-model="note.name" maxlength="50" />
                </div>
                <div class="form-group">
                    <label for="addContents">Contents</label>
                    <textarea id="addContents" class="form-control" v-model="note.contents" maxlength="1000" rows="10" />
                </div>
                <button id="addSave" @click="saveAdd()" class="btn btn-primary btn-sm mr-1">Save</button>
                <button id="addCancel" @click="cancelAdd()" class="btn btn-primary btn-sm">Cancel</button>
            </fieldset>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'

    export default {
        name: "Add",
        data() {
            return {
                note: {
                    name: '',
                    contents: ''
                }
            }
        },
        methods: {
            cancelAdd() {
                this.$router.push({ name: "home" });
            },
            async saveAdd() {
                try {
                    await axios.post('/api/notes', this.note);
                    this.$router.push({ name: "home" });
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            }
        }
    }
</script>
