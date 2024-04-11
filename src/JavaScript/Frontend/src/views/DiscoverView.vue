<script setup>
import { useTabsStore } from '../stores/useTabsStore.js'

const store = useTabsStore();
store.homePageActiveTab = 1;
</script>

<template>
    Tab 1
</template>