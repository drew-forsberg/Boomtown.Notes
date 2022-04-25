<template>
    <div class="row">
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
                    <td>
                        <button @click="$router.push({name: 'edit', params: { id: note.id }})" class="btn btn-primary btn-sm mr-1">Update</button>
                        <button @click="deleteNote(note.id)" class="btn btn-danger btn-sm">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <div>
            <button id="add" @click="$router.push({name: 'add'})" class="btn btn-primary btn-sm">Add New</button>
        </div>
    </div>
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
                if (!dateString) {
                    return '';
                }
                const date = new Date(dateString);
                return new Intl.DateTimeFormat('default', { dateStyle: 'short', timeStyle: 'short' }).format(date);
            },
            async getNotes() {

                try {
                    const response = await axios.get('/api/notes');
                    this.notes = response.data;
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            },
            async deleteNote(id) {

                try {
                    await axios.delete(`/api/notes/${id}`);
                    await this.getNotes();
                }
                catch (error) {
                    //Ideally, we'd present error messages in a more user-friendly manner
                    alert(error);
                }
            }
        },
        async created() {
            await this.getNotes();
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
