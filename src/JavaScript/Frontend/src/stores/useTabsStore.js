import { defineStore } from 'pinia'
import { ref } from "vue";
import { useRouter } from 'vue-router';

export const useTabsStore = defineStore('tabs', () => {
    const router = useRouter(); 
    const homePageTabList = ref([
        {
            name: '<span static-content="homepage-firsttab-title">تمام‌پاره ها</span><span class="icon-code-outline tabs-icon"></span>',
            action: () => {
                router.push({ name: 'discover' })
            }
        },
        {
            name: '<span static-content="homepage-secondtab-title">چندشاخه شده‌ها</span></span><span class="icon-code-fork tabs-icon"></span>',
            action: () => {
                router.push({ name: 'forked-discovery' })
            }
        },
        {
            name: '<span static-content="homepage-thirdtab-title">ستاره‌ خورده‌‌ها</span><span class="icon-star-empty tabs-icon"></span>',
            action: () => {
                router.push({ name: 'starred-discovery' })
            }
        }
    ]); 
    
    var homePageActiveTab = ref(1);
  
    return { homePageTabList, homePageActiveTab }
  })