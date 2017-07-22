const routes = [
    { path: '/accounts/:id', component: AccountDetail, props: true },
    { path: '/accounts', component: Accounts },
    { path: '/home', component: Home },
    { path: '*', redirect: '/home' }]

const router = new VueRouter({ routes })

Vue.http.options.root = 'http://localhost:9020/api';
Vue.http.interceptors.push(function (request, next) {
    var token = store.getToken();
    if (token != null) {
        request.headers.set('Authorization', store.getToken());
    }
    next();
});

const app = new Vue({
    data: {
        state: store.state
    },
    router
}).$mount('#app');
