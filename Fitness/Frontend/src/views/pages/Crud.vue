<template>
    <div class="bg-light d-flex flex-row align-items-center">
    <CContainer style="margin-top: 50px;">
      <CRow class="justify-content-center">
        <CCol md="8">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody class="form">
                <CForm>
                  <h1  style="margin-bottom: 20px" >{{isAdd ? 'Add medicine':'Update medicine'}} </h1>                 
                  <CFormLabel for="name" style="display: block;">Name</CFormLabel>
                  <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                    <CFormInput  id="name" placeholder="Please insert name" v-model="name" />
                  </CInputGroup>

                  <CFormLabel for="sname">Substance</CFormLabel>
                  <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                    <CFormInput id="sname" placeholder="Please insert substance" v-model="substance"/>
                  </CInputGroup>

                  <CFormLabel for="dose">Dose</CFormLabel>
                  <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                    <CFormInput id="dose" placeholder="Please insert dose" v-model="dose"/>
                    <CInputGroupText>mg/ml</CInputGroupText>
                  </CInputGroup>
                  <CFormLabel for="dose">Dose per day</CFormLabel>
                  <CFormSelect class="select" v-model="dosesPerDay">
                    <option value="" disabled selected hidden>Please select number of dose per day</option>
                    <option value="1">One (on every 24h)</option>
                    <option value="2">Two (on every 12h)</option>
                    <option value="3">Three (on every 8h)</option>
                    <option value="4">Four (on every 6h)</option>
                  </CFormSelect>
                  <CFormTextarea
                      id="source"
                      label="Medicine Source"
                      placeholder="Please insert source"
                      rows="3"
                      v-model="source"
                  >
                  </CFormTextarea>
                  <div style="justify-content: center; display: flex;" v-show="isAdd">
                    <CForm class="radioButtons">
                      <CFormCheck inline type="radio" label="Kilograms" v-model="kgs" name="radioButton1" v-on:click="age=false" :checked="kgs" />
                      <CFormCheck inline type="radio" label="Age" v-model="age" name="radioButton2" v-on:click="kgs=false" :checked="age"/>
                    </CForm>
                  </div>
                  <!-- <div class="d-grid d-md-block" style="text-align: center; margin-top:40px">
                      <CButton color="dark" class="px-4" v-on:click="{kgs=true; age=false}" style="margin: 0 10px; width: 80px;">Kgs</CButton>  
                      <CButton color="dark" class="px-4" v-on:click="{age=true; kgs=false}" style="margin: 0 10px; width: 80px;" v-show="isAdd">Age</CButton>
                  </div> -->

                  <div v-show="kgs">
                    <CFormLabel for="mindose">Min Dose per kg</CFormLabel> 
                    <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                      <CFormInput id="mindose" placeholder="Please insert min dose" v-model="minDose"/> 
                      <CInputGroupText>mg/kg</CInputGroupText>
                    </CInputGroup>
                    <CFormLabel for="maxdose">Max Dose per kg</CFormLabel> 
                    <CInputGroup style="width:70%; margin-bottom: 15px !important" class="mb-4">
                      <CFormInput id="maxdose" placeholder="Please insert max dose" v-model="maxDose"/>
                      <CInputGroupText>mg/kg</CInputGroupText>
                    </CInputGroup>
                  </div>
                  <div v-show="age">
                    <CButton color="dark" class="px-4" style="margin-bottom: 10px; margin-top: 15px;" v-on:click="addAgeDose">Add dose</CButton>
                    <CTable caption="top" class="tbl" color="dark" striped>
                      <CTableHead>
                        <CTableRow>
                          <CTableHeaderCell class="test" v-for="(header, id) in tableData.columns" :key="id">{{ header.label }}</CTableHeaderCell>
                        </CTableRow>
                      </CTableHead>
                      <CTableBody>
                        <CTableRow v-for="(item, index) in tableData.items" :key="index">
                          <CTableDataCell class="test">{{ item.from + ' (' + item.ageType + ')' }}</CTableDataCell>
                          <CTableDataCell class="test">{{ item.to + ' (' + item.ageType + ')' }}</CTableDataCell>
                          <CTableDataCell class="test">{{ item.min }}</CTableDataCell>
                          <CTableDataCell class="test">{{ item.max }}</CTableDataCell>
                          <CTableDataCell class="test">
                            <CButton color="light" class="px-3" v-on:click="onDelete(item)">
                              <CIcon icon="cil-trash"/>
                            </CButton>
                          </CTableDataCell>
                        </CTableRow>
                      </CTableBody>
                    </CTable>
                    <AgeDoseModal
                      :modalData="modalData"
                    />
                  </div>
                  <div class="d-grid d-md-block" style="text-align: center; margin-top:40px">
                      <CButton color="light" class="px-4" v-on:click="cancel" style="margin: 0 10px">Cancel</CButton>  
                      <CButton color="dark" class="px-4" v-on:click="addMedicine" style="margin: 0 10px" v-show="isAdd">Add</CButton>
                      <CButton color="dark" class="px-4" v-on:click="uptMedicine" style="margin: 0 10px" v-show="isUpt">Update</CButton>  
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
import dataServices from '../../services/data_services';
import AgeDoseModal from '@/components/AgeDoseModal.vue';

export default {
    name: "Crud",
    components: { AgeDoseModal },
    data() {
        return {
            isUpt: false,
            isAdd: false,
            name: "",
            substance: "",
            dose: "",
            dosesPerDay: null,
            minDose: "",
            maxDose: "",
            source: "",
            kgs: false,
            age: false,
            modalData: {
              isVisible: false,
              resolve: null,
              reject: null
            },
            tableData: {
              columns: [
                  {
                      key: 'From',
                      label: 'From'
                  },
                  {
                      key: 'To',
                      label: 'To'
                  },
                  {
                      key: 'Min',
                      label: 'Min Dose (mg)'
                  },
                  {
                      key: 'Max',
                      label: 'Max Dose (mg)'
                  },
                  {
                      key: 'Actions',
                      label: 'Actions'
                  }
              ],
              items: []
            }
        };
    },
    methods: {
        openModal() {
          return new Promise((resolve, reject) => {
            this.modalData.isVisible = true;
            this.modalData.resolve = resolve,
            this.modalData.reject = reject
          })
        },

        addAgeDose() {
          this.openModal().then(
            (response) => {
              this.tableData.items.push(response);
              this.modalData.isVisible = false;
              this.modalData.reject = null;
              this.modalData.resolve = null; 
            }, 
            () => {
              this.modalData.isVisible = false;
              this.modalData.reject = null;
              this.modalData.resolve = null; 
            }
          );
        },

        addMedicine() {
          var request = {
              Name: this.name,
              Substance: this.substance,
              Source: this.source,
              Dose: this.dose,
              MinDoseByKg: this.minDose == "" ? -1 : this.minDose,
              MaxDoseByKg: this.maxDose == "" ? -1: this.maxDose,
              DosesPerDay: this.dosesPerDay,
              AgeRangeDoses: JSON.stringify(this.tableData.items)
          };
          let loader = this.$loading.show();
          dataServices.methods.add_medicine(request).then(() => {
              loader.hide();
              this.$router.push('/medicines');
          });
        },
        uptMedicine() {
          let id = this.$route.params.id;
          var request = {
              Name: this.name,
              Substance: this.substance,
              Source: this.source,
              Dose: this.dose,
              DosesPerDay: this.dosesPerDay,
              MinDoseByKg: this.minDose,
              MaxDoseByKg: this.maxDose,
              AgeRangeDoses: JSON.stringify(this.tableData.items)
          };
          let loader = this.$loading.show();
          dataServices.methods.upt_medicine(id, request).then(() => {
              loader.hide();
              this.$router.push('/medicines');
          });
        },
        handleUpdate() {
          let id = this.$route.params.id;
          dataServices.methods.get_medicine_by_id(id).then((response) => {
              this.name = response.data.name;
              this.substance = response.data.substance;
              this.dose = response.data.dose;
              this.maxDose = response.data.maxDoseByKg;
              this.minDose = response.data.minDoseByKg;
              this.source = response.data.source;
              this.dosesPerDay = response.data.dosesPerDay;
              if(response.data.ageRangeDosesExtracted && response.data.ageRangeDosesExtracted.length != 0) {
                this.tableData.items = response.data.ageRangeDosesExtracted;
                this.age = true;
              }
              else {
                this.kgs = true;
              }
          });
        },
        cancel() {
          this.$router.push('/medicines');
        },
        onDelete(item) {
          var newItems = [];
          for(let i = 0; i < this.tableData.items.length; i++)
            if(this.tableData.items[i] != item)
              newItems.push(this.tableData.items[i]);
          this.tableData.items = newItems;
          console.log(this.tableData.items);
          console.log(newItems);
        }
    },
    mounted() {
      if (this.$route.params.id == 0)
          this.isAdd = true;
      else {
          this.isUpt = true;
          this.handleUpdate();
      }
    },
}

</script>

<style>
.container-lg {
  margin-top: 50px !important;
}

.select {
  width:70%; 
  margin-bottom: 15px !important;
}

.select-red {
  width:70%; 
  margin-bottom: 15px !important;
  border-radius: 1px;
  border-color: red;
}

.radioButtons {
  padding-top: 7px;
  margin-top: 50px;
  margin-bottom: 50px;
  justify-content: center;
  align-items: center;
  display: flex;
  width: 30%;
  border: 1px solid black;
  border-radius: 10px;
}

.tbl {
  width: 100%;
  border: 1px solid black;
}

.test {
  border: 1px solid black;
  text-align: center;
}
</style>