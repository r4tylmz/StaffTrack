import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import router from "@/router";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    email: '',
    password: '',
    token: '',
  },
  getters: {
    isAuthenticated(state){
      return state.token !== '';
    }
  },
  mutations: {
    setToken(state, token){
      state.token = token;
    },
    clearToken(state){
      state.token = '';
    }
  },
  actions: {
    initialize({commit}){
      let token = localStorage.getItem("token");
      if (token){
        commit("setToken",token);
        router.push("/");
      }
      else{
        router.push("/login");
        return false;
      }
    },
    login({commit}, authData) {
      axios
        .post(`https://localhost:5001/api/User/login`, { ...authData
        })
        .then((response) => {
          if (response.status === 200) {
            commit("setToken", response.data.id);
            localStorage.setItem("token",response.data.id);
            router.push("/");
          }
        });
    },
    logout({commit}){
      commit("clearToken");
      localStorage.removeItem("token");
    }
  },
});
