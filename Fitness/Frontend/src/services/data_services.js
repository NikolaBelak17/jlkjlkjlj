import axios from "axios";

const url = process.env.VUE_APP_API_URL;
const TRAINERS = "http://localhost:8000";

export default {
    methods: {
        login(user, pw) {
            const data = new FormData();
            data.append('username', user);
            data.append('password', pw);
            return axios.post("http://localhost:4000/api/v1/authentication/Login", data);              
        },

        register(firstname, lastname, username, password, email, phonenumber) {
            const data = new FormData();
            data.append('firstname', firstname);
            data.append('lastname', lastname);
            data.append('username', username);
            data.append('password', password);
            data.append('email', email);
            data.append('phonenumber', phonenumber);

            return axios.post("http://localhost:8080/api/v1/authentication/RegisterClient");
        },

        add_medicine(request) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.post(url + "medicines", request);          
        },

        upt_medicine(med_id, request) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.put(url + `medicines/${med_id}`, request);
        },

        remove_medicine(med_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.delete(url + `medicines/${med_id}`);
        },

        get_medicines() {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.get(url + 'medicines');                      
        },

        get_medicine_by_id(med_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.get(url + `medicines/${med_id}`);
        },

        add_infusion(request) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.post(url + "infusions", request);          
        },

        upt_infusion(inf_id, request) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.put(url + `infusions/${inf_id}`, request);
        },

        remove_infusion(inf_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.delete(url + `infusions/${inf_id}`);
        },

        get_infusions() {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.get(url + 'infusions');                       
        },

        get_infusion_by_id(inf_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.get(url + `infusions/${inf_id}`);
        },
        get_trainers(inf_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.get(`${TRAINERS}/api/v1/Trainer`);
        },
        add_trainer(request) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.post(`${TRAINERS}/api/v1/Trainer`, request);          
        },
        upt_trainer(tra_id, request) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.put(`${TRAINERS}/api/v1/Trainer`, request);
        },

        get_trainer_by_id(tra_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.get(`${TRAINERS}/api/v1/Trainer/${tra_id}`);
        },

        remove_trainer(tra_id) {
            axios.defaults.headers.common = {'Authorization': `Bearer ${sessionStorage.getItem('pharmacyToken')}`};
            return axios.delete(`${TRAINERS}/api/v1/Trainer/${tra_id}`);
        },
    }
}
