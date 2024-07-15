<template>
  <div class="bg-light min-vh-100 d-flex flex-row align-items-center">
    <CContainer>
      <CRow class="justify-content-center">
        <CCol :md="4">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody>
                <CForm>
                  <div style="text-align:center; margin-botom:10px">
                    <img src="/pharmacy_logo.png" />
                  </div>
                  <h2>Login</h2>
                  <p class="text-medium-emphasis">Sign In to your account</p>
                  <CInputGroup class="mb-3">
                    <CInputGroupText>
                      <CIcon icon="cil-user" />
                    </CInputGroupText>
                    <CFormInput
                      placeholder="Username"
                      autocomplete="username"
                      v-model="username"
                      :class = "formVars.username.class"
                    />
                  </CInputGroup>
                  <CInputGroup class="mb-4">
                    <CInputGroupText>
                      <CIcon icon="cil-lock-locked" />
                    </CInputGroupText>
                    <CFormInput
                      type="password"
                      placeholder="Password"
                      autocomplete="current-password"
                      v-model="password"
                      :class = "formVars.password.class"
                    />
                  </CInputGroup>
                  <CRow>
                    <CCol :xs="6">
                      <CButton color="primary" class="px-4" v-on:click="change"> Login </CButton>
                    </CCol>
                  </CRow>
                </CForm>
              </CCardBody>
            </CCard>
          </CCardGroup>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import dataServices from '../../services/data_services';
import axios from "axios";

export default {
  name: 'Login',
  data() {
    return {
      username: "",
      password: "",
      wrongAcc: false,
      forgotPw: true,
      formVars: {
        username: {
          class: ""
        },
        password: {
          class: ""
        }
      },
      isLoading: true    
    }
  },
  methods: {
    change(event) {
      if(!this.validateUserInfo) {
        
      }
      else {
        sessionStorage.setItem('pharmacyToken', 'hardcoded');
        this.$router.push('/medicines');
        //let loader = this.$loading.show();
        //dataServices.methods.login(this.username, this.password).then((response) => { 
        //  sessionStorage.setItem('pharmacyToken', response.data.access_token);
        //  loader.hide();
        //  this.$router.push('/medicines');
        //}, 
        //() => {
        //  loader.hide()
        //  this.wrongAcc = true;
        //  this.forgotPw = false;
        //});  
      }
    }
  },

  computed: {
    validateUserInfo() {
            let isValid = true;
            if(this.username.trim().length <= 0) {
              isValid = false;
              this.formVars.username.class = "error";
            }
            else 
              this.formVars.username.class = "";
            
            if(this.password.trim().length <= 0) {
              isValid = false;
              this.formVars.password.class = "error";
            }
            else
              this.formVars.password.class = "";
            
            if(!isValid)
              alert("Empty username or password!");
            return isValid;
    }
  },
  created() {
  }
}
</script>

<style>
.error {
  border-color: red;
}

</style>
