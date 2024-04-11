<script setup>
</script>

<template>
        <footer>
            <img class="noselect" src="../assets/images/logo.svg">
            <span>
                © {{ new Date().getFullYear() }} MahdiyarGHD
            </span>
            <a>
                شرایط و مقررات
            </a>
            <a>
                حریم خصوصی
            </a>
            <a>
                امنیت
            </a>
            <a>
                ارتباط
            </a>
        </footer>
</template>

<style scoped>
footer {
    height: 40px;
    padding: 20px 0px;
    margin: 10px auto;
    flex-direction: row-reverse;
    display: flex;  
    justify-content: center;
    align-items: center;
}

footer * {
    text-align: right;
    direction: rtl;
    margin: 0px 0.69em;
    display: inline-block;
}

footer img {
    opacity: 50%;
    width: 19px;
}

:is(span, a) {
    font-size: 12px;
    color: #999999
}

a:hover {
    text-decoration: underline;
    cursor: pointer;
}
</style>