<template>
    <h1 id="tableLabel">Notes</h1>

    <p v-if="!notes"><em>Loading...</em></p>

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
                <td><button @click="saveNote(note)">Save</button></td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import axios from 'axios'
    export default {
        name: "Home",
        data() {
            return {
                notes: []
            }
        },
        methods: {
            formatDate(dateString) {
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
            async saveNote(note) {
                try {
                    await axios.post('api/notes', note);
                    await this.getNotes();
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            }
        },
        mounted() {
            this.getNotes();
        }
    }
</script>

<style scoped>
    th, td {
        text-align: left
    }
</style>