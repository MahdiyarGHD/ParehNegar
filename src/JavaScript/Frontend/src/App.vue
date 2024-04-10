<script setup>
import { RouterLink, RouterView, useRouter } from 'vue-router';
import { onMounted } from 'vue';
import Header from './components/Header.vue'
import Tabs from "./components/Tabs.vue";
import { useTabsStore } from './stores/useTabsStore.js'
import StaticContents from './components/StaticContents.vue';
const store = useTabsStore();
</script>

<template>
  <Header />
  <h3 class="home-headline" static-content="homepage-headline">بلافاصله کد، یادداشت و قطعه‌های کد را به اشتراک بگذارید.</h3>
    <h3 class="home-tabs-title"><span static-content="homepage-discover">کاوش پاره‌ها</span><span class="icon-code-outline"></span></h3>
    <div class="tabs-container">
        <Tabs
            class="tabs"
            :tabList="store.homePageTabList"
            :activeTab="store.homePageActiveTab"
        />
    </div>
    <div class="main-container">
      <RouterView />
    </div>
    
    <StaticContents/>
</template>

<style scoped>
.home-headline {
    margin-top: 35px;
    width: 100%;
    text-align: center;
    padding: 0 10px;
    font-weight: 300;
    padding-bottom: 70px;
    direction: rtl;
}

.home-tabs-title {
    font-size: 22px;
    position: relative;
    margin-top: 35px;
    padding-bottom: 14px;
    width: 100%;
    text-align: right;
    font-weight: normal;
    padding-right: 65px;
    direction: rtl;
}

.home-tabs-title .icon-code-outline {
    position: absolute;
    font-size: 34px;
    bottom: 10px;
    right: 0.70em;
}
</style>