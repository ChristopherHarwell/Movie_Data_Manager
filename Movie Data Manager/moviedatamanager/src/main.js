import Vue from 'vue';
import MovieTable from "./components/MovieTable.vue";

Vue.use(MovieTable);

Vue.config.productionTip = false;

new Vue({
    el:'#app',
    render: h => h(MovieTable)
})