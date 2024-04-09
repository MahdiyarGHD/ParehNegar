<script setup>
import { RouterLink, RouterView, useRouter } from 'vue-router';
import Header from './components/Header.vue'
import Tabs from "./components/Tabs.vue";
import { useTabsStore } from './stores/useTabsStore.js'
import contentApi from './client/contentApi.js'
window.addEventListener("load", function() { 
  const container = document.getElementById("app");
  const elements = container.querySelectorAll("[static-content]");
  
  elements.forEach(element => {
    const button = document.createElement("button");
    button.textContent = "Edit"; 
    button.style.position = "absolute";
    button.classList.add("content-edit-btn"); 
    button.style.top = element.offsetTop + "px"; 
    button.style.left = element.offsetLeft + "px"; 
    button.id = `edit-content-btn__${element.getAttribute("static-content")}`;
    
    button.addEventListener('click', (ele) => {
      const textarea = document.createElement("textarea");
      contentApi.apiContentGetByLanguagePost({
        body: { 
          key: element.getAttribute("static-content"),
          language: ""
        }
      }, (err, response) => textarea.innerHTML = response?.result?.data ?? "")
      
      textarea.style.position = "absolute";
      textarea.style.top = element.offsetTop + "px"; 
      textarea.style.left = element.offsetLeft + "px"; 
      textarea.id = `edit-content-input__${element.getAttribute("static-content")}`;
      container.appendChild(textarea);
      
      const saveButton = document.createElement("button");
      saveButton.textContent = "Save"; 
      saveButton.style.position = "absolute";
      saveButton.classList.add("content-edit-btn"); 
      saveButton.style.top = textarea.offsetTop + 20 + "px"; 
      saveButton.style.left = textarea.offsetLeft + "px"; 
      saveButton.id = `save-content-btn__${element.getAttribute("static-content")}`;
      
      saveButton.addEventListener('click', (el) => {
        contentApi.apiContentAddContentWithKeyPost({
          body: {
            key: element.getAttribute("static-content"),
            languageData: [
              {
                language: "fa-IR",
                data: textarea.value
              }
            ]
          }
        }, (err, response) => {
          saveButton.remove();
          textarea.remove();
        })
      })
      
      container.appendChild(saveButton);
      
    });

    container.appendChild(button);
    
    
      contentApi.apiContentGetByLanguagePost({
            body: { 
              key: element.getAttribute("static-content"),
              language: ""
            }
      }, (err, response) => element.innerHTML = response?.result?.data)
  });
  
});



const store = useTabsStore();
</script>

<template>
  <Header />
  <h3 class="home-headline" static-content="homepage-headline">بلافاصله کد، یادداشت و قطعه‌های کد را به اشتراک بگذارید.</h3>
    <h3 class="home-tabs-title">کاوش پاره‌ها <span class="icon-code-outline"></span></h3>
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