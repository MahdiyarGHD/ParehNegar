<script setup>
import contentApi from '../client/contentApi.js'
import { onMounted } from 'vue';


onMounted(() => {
  const container = document.getElementById("app");
  const elements = container.querySelectorAll("[static-content]");
  elements.forEach(element => {
    const button = document.createElement("button");
    var rect = element.getBoundingClientRect();
    button.textContent = "Edit"; 
    button.style.position = "absolute";
    button.classList.add("content-edit-btn"); 
    button.style.top = rect.top + "px"; 
    button.style.left = rect.left + "px"; 
    button.id = `edit-content-btn__${element.getAttribute("static-content")}`;
    
    button.addEventListener('click', (ele) => {
      if(document.getElementById(`edit-content-input__${element.getAttribute("static-content")}`) == undefined) {
        const textarea = document.createElement("textarea");
        contentApi.apiContentGetByLanguagePost({
          body: { 
            key: element.getAttribute("static-content"),
            language: ""
          }
        }, (err, response) => textarea.innerHTML = response?.result?.data ?? "")

        textarea.style.position = "absolute";
        textarea.style.top = rect.top + "px"; 
        textarea.style.left = rect.left + (rect.left > window.innerWidth/2 ? -window.innerWidth * (4/100) : +window.innerWidth * (4/100) ) + "px"; 
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
          }, () => {
            element.innerHTML = textarea.value
            saveButton.remove();
            textarea.remove();
          })
        })

        container.appendChild(saveButton);
        }
      
    });

    container.appendChild(button);
    
    contentApi.apiContentGetByLanguagePost({
          body: { 
            key: element.getAttribute("static-content"),
            language: ""
          }
    }, (err, response) => element.innerHTML = response?.result?.data ?? element.getAttribute("static-content"))
  });
})
</script>

<template></template>