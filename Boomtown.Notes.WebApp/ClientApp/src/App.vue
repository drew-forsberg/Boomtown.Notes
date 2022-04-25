<template>
    <div class="container">
        <nav class="row mb-3">
            <router-link to="/">Home</router-link> <span class="mr-2 ml-2">|</span>
            <router-link to="/add">Add</router-link>
        </nav>

        <router-view />
    </div>
</template>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: left;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
