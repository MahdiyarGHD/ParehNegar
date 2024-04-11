<script setup>
import { useTabsStore } from '../stores/useTabsStore.js'
const store = useTabsStore();
store.homePageActiveTab = 2;
</script>

<template>
    Tab 2
</template>