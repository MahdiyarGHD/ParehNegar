<script setup>
import { useTabsStore } from '../stores/useTabsStore.js'
const store = useTabsStore();
store.homePageActiveTab = 3;
</script>

<template>
    Tab 3
</template>

