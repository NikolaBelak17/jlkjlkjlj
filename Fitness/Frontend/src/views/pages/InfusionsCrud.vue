<template>
    <div class="bg-light d-flex flex-row align-items-center">
    <CContainer style="margin-top: 50px;">
      <CRow class="justify-content-center">
        <CCol md="8">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody class="form">
                <CForm>
                  <h1  style="margin-bottom: 20px" >{{isAdd ? 'Add infusion' : 'Update infusion'}} </h1>                 
                  <CFormLabel for="name" style="display: block;">Name</CFormLabel>
                  <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                    <CFormInput  id="name" placeholder="Please insert name" v-model="name" />
                  </CInputGroup>

                  <CFormLabel for="sname">Substance</CFormLabel>
                  <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                    <CFormInput id="sname" placeholder="Please insert substance" v-model="substance"/>
                  </CInputGroup>
                  <CFormLabel for="idose">Dose</CFormLabel>
                  <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                    <CFormInput id="idose" placeholder="Please insert dose" v-model="dose"/>
                    <CInputGroupText>mg/ml</CInputGroupText>
                  </CInputGroup>
                  <div style="border: 1px solid black; border-radius: 5px; padding-left: 5px; width: 70%; padding-right: 5px; margin-bottom: 15px;">
                    <legend>Initial Dose</legend>
                    <CFormLabel for="idose">Min Dose per kg</CFormLabel>
                    <CInputGroup style="margin-bottom: 15px !important" class="mb-4">
                      <CFormInput id="idose" placeholder="Please insert min dose" v-model="minInitDose"/>
                      <CInputGroupText>mg/kg</CInputGroupText>
                    </CInputGroup>
                    <CFormLabel for="idose">Max Dose per kg</CFormLabel>
                    <CInputGroup style="margin-bottom: 15px !important" class="mb-4">
                      <CFormInput id="idose" placeholder="Please insert max dose" v-model="maxInitDose"/>
                      <CInputGroupText>mg/kg</CInputGroupText>
                    </CInputGroup>
                    <CFormLabel for="dose">Dose per day</CFormLabel>
                    <CFormSelect style="margin-bottom: 15px !important" v-model="initDosesPerDay">
                      <option value="" disabled selected hidden>Please select number of dose per day</option>
                      <option value="1">One (on every 24h)</option>
                      <option value="2">Two (on every 12h)</option>
                      <option value="3">Three (on every 8h)</option>
                      <option value="4">Four (on every 6h)</option>
                      <option value="6">Six (on every 4h)</option>
                    </CFormSelect>
                  </div>
                  <div style="border: 1px solid black; border-radius: 5px; padding-left: 5px; width: 70%; padding-right: 5px; margin-bottom: 15px;">
                    <legend>Regular Dose</legend>
                    <CFormLabel for="rdose">Min Dose per kg</CFormLabel>
                    <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                      <CFormInput id="rdose" placeholder="Please insert min dose" v-model="minRegDose"/>
                      <CInputGroupText>mg/kg</CInputGroupText>
                    </CInputGroup>
                    <CFormLabel for="rdose">Max Dose per kg</CFormLabel>
                    <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                      <CFormInput id="rdose" placeholder="Please insert max dose" v-model="maxRegDose"/>
                      <CInputGroupText>mg/kg</CInputGroupText>
                    </CInputGroup>
                    <CFormLabel for="dose">Dose per day</CFormLabel>
                    <CFormSelect style="width:70%; margin-bottom: 15px !important" v-model="regDosesPerDay">
                      <option value="" disabled selected hidden>Please select number of dose per day</option>
                      <option value="1">One (on every 24h)</option>
                      <option value="2">Two (on every 12h)</option>
                      <option value="3">Three (on every 8h)</option>
                      <option value="4">Four (on every 6h)</option>
                      <option value="6">Six (on every 4h)</option>
                    </CFormSelect>
                  </div>
                  <CFormTextarea
                      id="source"
                      label="Infusion Source"
                      placeholder="Please insert source"
                      rows="3"
                      v-model="source"
                  >
                  </CFormTextarea>
                  <div class="d-grid d-md-block" style="text-align: center; margin-top:40px">
                      <CButton color="light" class="px-4" v-on:click="cancel" style="margin: 0 10px">Cancel</CButton>  
                      <CButton color="dark" class="px-4" v-on:click="addInfusion" style="margin: 0 10px" v-show="isAdd">Add</CButton>
                      <CButton color="dark" class="px-4" v-on:click="uptInfusion" style="margin: 0 10px" v-show="isUpt">Update</CButton>  
                  </div>
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
import dataServices from '../../services/data_services'

export default {
    data() {
        return {
            isUpt: false,
            isAdd: false,
            name: "",
            substance: "",
            dose: "",
            initDosesPerDay: null,
            regDosesPerDay: null,
            minInitDose: "",
            maxInitDose: "",
            minRegDose: "",
            maxRegDose: "",
            source: "",
        }
    },

    methods: {
      addInfusion() {
        var request = {
          Name: this.name,
          Substance: this.substance,
          Source: this.source,
          MinInitDose: this.minInitDose,
          MaxInitDose: this.maxInitDose,
          InitDosesPerDay: this.initDosesPerDay,
          MinRegDose: this.minRegDose,
          MaxRegDose: this.maxRegDose,
          RegDosesPerDay: this.regDosesPerDay,
          Dose: this.dose
        };

        let loader = this.$loading.show();
        dataServices.methods.add_infusion(request).then( () => {
          loader.hide();
          this.$router.push('/infusions');
        });        
      },

      uptInfusion() {      
        let id = this.$route.params.id;

        var request = {
          Name: this.name,
          Substance: this.substance,
          Source: this.source,
          MinInitDose: this.minInitDose,
          MaxInitDose: this.maxInitDose,
          InitDosesPerDay: this.initDosesPerDay,
          MinRegDose: this.minRegDose,
          MaxRegDose: this.maxRegDose,
          RegDosesPerDay: this.regDosesPerDay,
          Dose: this.dose
        };

        let loader = this.$loading.show();
        dataServices.methods.upt_infusion(id, request).then( () => {
          loader.hide();
          this.$router.push('/infusions'); 
        });       
      },

      handleUpdate() {
        let id = this.$route.params.id;

        dataServices.methods.get_infusion_by_id(id).then( (response) => {

          this.name = response.data.name;
          this.substance = response.data.substance;
          this.source = response.data.source;
          this.minInitDose = response.data.minInitDose;
          this.maxInitDose = response.data.maxInitDose;
          this.initDosesPerDay = response.data.initDosesPerDay;
          this.minRegDose = response.data.minRegDose;
          this.maxRegDose = response.data.maxRegDose;
          this.regDosesPerDay = response.data.regDosesPerDay;
          this.dose = response.data.dose;
        });
      },
      cancel() {
         this.$router.push('/infusions'); 
      }
    },

    created() {
        if(this.$route.params.id == 0)
            this.isAdd = true;
        else {
            this.isUpt = true;
            this.handleUpdate();
        }
    }
    
}

</script>

<style>
.container-lg {
  margin-top: 50px !important;
}
</style>