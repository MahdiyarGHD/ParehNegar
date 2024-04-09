import { defineStore } from 'pinia'
import { ref } from "vue";
import { useRouter } from 'vue-router';

export const useTabsStore = defineStore('tabs', () => {
    const homePageTabList = ref([
        {
            name: 'تمام‌پاره ها <span class="icon-code-outline tabs-icon"></span>',
            action: () => {
                this.$emit('navigate-to-home');
            }
        },
        {
            name: 'چند شاخه‌ شده‌ها <span class="icon-code-fork tabs-icon"></span>',
            action: () => {
                router.push({ path: 'forked-discovery' })
            }
        },
        {
            name: 'ستاره‌ خورده‌‌ها <span class="icon-star-empty tabs-icon"></span>',
            action: () => {
                router.push({ path: 'home' })
            }
        }
    ]); 
    
    var homePageActiveTab = ref(1);
  
    return { homePageTabList, homePageActiveTab }
  })