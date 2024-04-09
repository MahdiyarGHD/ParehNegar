import { defineStore } from 'pinia'
import { ref } from "vue";
import { useRouter } from 'vue-router';

export const useTabsStore = defineStore('tabs', () => {
    const router = useRouter(); 
    const homePageTabList = ref([
        {
            name: 'تمام‌پاره ها <span class="icon-code-outline tabs-icon"></span>',
            action: () => {
                router.push({ name: 'discover' })
            }
        },
        {
            name: 'چند شاخه‌ شده‌ها <span class="icon-code-fork tabs-icon"></span>',
            action: () => {
                router.push({ name: 'forked-discovery' })
            }
        },
        {
            name: 'ستاره‌ خورده‌‌ها <span class="icon-star-empty tabs-icon"></span>',
            action: () => {
                router.push({ name: 'starred-discovery' })
            }
        }
    ]); 
    
    var homePageActiveTab = ref(1);
  
    return { homePageTabList, homePageActiveTab }
  })