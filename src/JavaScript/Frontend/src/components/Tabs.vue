<script setup>
import { ref } from 'vue';

const props = defineProps({
  tabList: {
    type: Array,
    required: true,
  },
  activeTab: {
    type: Number,
    required: true
  }
});

// const activeTab = ref(1);
</script>
  
<template>
    <div
      class="flex space-x-4"
    >
      <ul
        class="list-none bg-blue-900 bg-opacity-30 p-1.5 rounded-lg text-center overflow-auto whitespace-nowrap flex items-center mb-6"
      >
        <li
          v-for="(tab, index) in tabList"
          :key="index"
          class="noselect"
          :class="{
            'tab-select' : (index + 1) == activeTab 
          }"
        >
            <a
            :id="`${index}-tab`"
            :name="`${index}-tab`"
            type="button"
            @click="{
                tab.action();
            }"
            v-html="tab.name"
            />
        </li>
      </ul>
      <hr>
    </div>
</template>
  
<style scoped>
ul {
    padding: 0px 15px;
    display: flex;  
    justify-content: right;
    flex-direction: row-reverse;
    align-items: normal;
}

li {
    position: relative;
    margin-right: 5px;
    padding-bottom: 10px;
    border-bottom: 2px solid transparent;
}

a {
    cursor: pointer;
    display: block;
    padding: 0.55rem 0.55rem;
    padding-right: 34px;
    transition: all 100ms ease;
}

a:hover {
    background-color: #2a3042;
    border-radius: 7px;
}

hr {
    border: none;
    border-bottom: 2px solid #2a3042;
}

.tab-select {
    border-bottom-color: #F78166;
    font-weight: normal;
}
</style>