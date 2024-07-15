<template>
    <CForm>
      <CButton color="dark" class="px-4" v-on:click="addInfusion" style="margin-right: 973px;">Add infusion</CButton>
    </CForm>
    <CTable caption="top" class="tbl" color="dark" striped>
      <CTableCaption style="font-weight: 600;">LIST OF INFUSIONS</CTableCaption>
      <CTableHead>
        <CTableRow>
          <CTableHeaderCell class="test">{{ headers.id }}</CTableHeaderCell>
          <CTableHeaderCell class="test">{{ headers.name }}</CTableHeaderCell>
          <CTableHeaderCell class="test">{{ headers.substance }}</CTableHeaderCell>
          <CTableHeaderCell class="test">{{ headers.dose }}</CTableHeaderCell>
          <CTableHeaderCell class="test">{{ headers.date }}</CTableHeaderCell>
          <CTableHeaderCell class="test">{{ headers.actions }}</CTableHeaderCell>
        </CTableRow>
      </CTableHead>
      <CTableBody>
        <CTableRow v-for="(inf, index) in infusions" :key="index">
          <CTableDataCell class="test">{{inf.id}}</CTableDataCell>
          <CTableDataCell class="test">{{inf.name}}</CTableDataCell>
          <CTableDataCell class="test">{{inf.substance}}</CTableDataCell>
          <CTableDataCell class="test">{{inf.dose}}</CTableDataCell>
          <CTableDataCell class="test">{{inf.date}}</CTableDataCell>
          <CTableDataCell class="test">
            <CButton color="light" class="px-3" style="margin: 0 10px;" v-on:click="updateInfusion(inf.id)">
              <CIcon icon="cil-pencil"/>
            </CButton>
            <CButton color="light" class="px-3" v-on:click="onDelete(inf.id)">
              <CIcon icon="cil-trash"/>
            </CButton>
          </CTableDataCell>
        </CTableRow>
      </CTableBody>
    </CTable>
    <GenericModal :modalData="modalData"/>
  </template>
  
  <script>
  import dataServices from '../../services/data_services'
  import GenericModal from '../../components/GenericModal.vue'
  
  export default {
      name: "Infusions",
      components: { 
        GenericModal 
      },
      data() {
          return {
              headers: {
                  id: "Id",
                  name: "Name",
                  substance: "Substance",
                  dose: "Dose",
                  date: "Last Updated Date",
                  actions: "Actions"
              },
              infusions: [],
              modalData: {
                isVisible: false,
                title: "Confirm delete",
                body: "Are you sure that you want to delete this infusion?",
                resolve: null,
                reject: null
              }
          };
      },
      methods: {
          addInfusion() {             
            this.$router.push('/infusions/0');
          },
          removeInfusion(id) {
            let loader = this.$loading.show();
            dataServices.methods.remove_infusion(id).then(() => {
              console.log('Succesful delete');
              dataServices.methods.get_infusions().then( (response) => {this.infusions = response.data; loader.hide()});
          });
            
          },
  
          updateInfusion(id) {
            this.$router.push('/infusions/' + id);
          },
  
          onDelete(id) {
            this.openModal().then((result) => {
              if(result) {
                this.removeInfusion(id);
              }
              this.modalData.isVisible = false;
              this.modalData.resolve = null;
              this.modalData.reject = null;
            });
          },
  
          openModal() {
            return new Promise((resolve, reject) => {
              this.modalData.isVisible = true;
              this.modalData.resolve = resolve;
              this.modalData.reject = reject;
            }) 
             
          }
      },
      mounted() {
        //let loader = this.$loading.show();
        //dataServices.methods.get_infusions().then(x => {this.infusions = x.data; loader.hide()});
      }
  }  
  </script>
  
  <style>
    #txt {
      font-size: 14px;
      color: red;
      text-align: center;
      font-family: Verdana, Geneva, Tahoma, sans-serif;
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