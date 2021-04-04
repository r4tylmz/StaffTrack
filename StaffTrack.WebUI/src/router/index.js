import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../components/Home.vue";
import UserInit from "../components/UserInit.vue";
import UserDetail from "../components/UserDetail.vue";
import User from "../components/User.vue";
import Login from "../components/Login.vue";
import Admin from "../components/Admin.vue";
import {store} from "@/store";

Vue.use(VueRouter);

const routes = [
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/",
    name: "Home",
    component: Home,
    beforeEnter(to, from, next) {
      if (store.getters.isAuthenticated) {
        next();
      } else {
        next("/login");
      }
    },
  },
  {
    path: "/users",
    name: "User",
    children: [
      { path: "/", name: "UserInit", component: UserInit },
      { path: ":id", name: "UserDetail", component: UserDetail },
    ],
    component: User,
    beforeEnter(to, from, next) {
      if (store.getters.isAuthenticated) {
        next();
      } else {
        next("/login");
      }
    },
  },
  { name: "Admin", path: "/admins", 
    component: Admin,
    beforeEnter(to, from, next) {
      if (store.getters.isAuthenticated) {
        next();
      } else {
        next("/login");
      }
    },
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
