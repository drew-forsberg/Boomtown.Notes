<template>
    <header>
        <nav class="navbar navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <a class="navbar-brand">Boomtown Notes App by Drew M. Forsberg</a>
            </div>
        </nav>
    </header>
</template>

<style>
    a.navbar-brand {
        white-space: normal;
        text-align: center;
        word-break: break-all;
    }

    html {
        font-size: 14px;
    }

    @media (min-width: 768px) {
        html {
            font-size: 16px;
        }
    }

    .box-shadow {
        box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
    }
</style>
<script>
    export default {
        name: "NavMenu"
    }
</script>