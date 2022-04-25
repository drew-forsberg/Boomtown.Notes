import { createRouter, createWebHistory } from 'vue-router'

import Home from '../components/Home.vue';
import Add from '../components/Add.vue';
import Edit from '../components/Edit.vue';

const routes = [
    { path: '/', component: Home, name: "home" },
    { path: '/add', component: Add, name: "add" },
    {
        path: '/edit/:id', component: Edit, name: "edit", props: true
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;