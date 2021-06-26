import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import router from "@/router";

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        admin: {
            id: 0,
            name: "",
            lastName: "",
            email: "",
        },
        token: '',
    },
    getters: {
        isAuthenticated(state) {
            return state.token !== '';
        },
        getAdminCredentials(state) {
            return state.admin;
        }
    },
    mutations: {
        setAdminCredentials(state, admin) {
            state.admin = admin;
        },
        setToken(state, token) {
            state.token = token;
        },
        clearToken(state) {
            state.token = '';
        }
    },
    actions: {
        initialize({commit, dispatch}) {
            let token = localStorage.getItem("token");
            if (token) {
                commit("setToken", token);
                dispatch("setAdminCredentials");
                router.push("/users");

            } else {
                router.push("/login");
                return false;
            }
        },
        login({commit}, authData) {
            axios
                .post(`https://localhost:5001/api/User/login`, {
                    ...authData
                })
                .then((response) => {
                    if (response.status === 200) {
                        commit("setToken", response.data.id);
                        this.admin = response.data;
                        localStorage.setItem("token", response.data.id);
                        router.push("/users");
                    }
                });
        },
        setAdminCredentials({state, commit}) {
            axios.get(`https://localhost:5001/api/User/${state.token}`)
                .then(response => {
                    commit("setAdminCredentials", response.data);
                });
        },
        logout({commit}) {
            commit("clearToken");
            localStorage.removeItem("token");
            router.push("/login");
        }
    },
});
