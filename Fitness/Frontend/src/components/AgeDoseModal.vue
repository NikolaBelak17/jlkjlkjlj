<template>
    <CModal :visible="modalData.isVisible" @close="onClose">
        <CModalHeader> 
            <h5 style="margin-right: 15px;">Age-range dose</h5>
        </CModalHeader>
        <CModalBody style="display: flex; flex-direction: column; align-items: center;">
            <CFormSelect class="select" style="width: 35%;" v-model="ageType">
                    <option value="" disabled selected hidden>Select age type</option>
                    <option value="yrs">Years</option>
                    <option value="mth">Months</option>
            </CFormSelect>

            <CInputGroup style="width:100%; margin-bottom: 15px !important; justify-content: space-around;" class="mb-4">
                <div style="width: 40%; display: flex;">
                    <CFormInput id="from" placeholder="From" v-model="from" style="border-top-right-radius: 0; border-bottom-right-radius: 0;"/>
                    <CInputGroupText style="width: 68px; border-top-left-radius: 0; border-bottom-left-radius: 0;">{{ ageType }}</CInputGroupText>
                </div>
                <div style="width: 40%; display: flex;">
                    <CFormInput id="to" placeholder="To" v-model="to" style="border-top-right-radius: 0; border-bottom-right-radius: 0;"/>
                    <CInputGroupText style="width: 68px;  border-top-left-radius: 0; border-bottom-left-radius: 0;">{{ ageType }}</CInputGroupText>
                </div>
            </CInputGroup>

            <CInputGroup style="width:100%; margin-bottom: 15px !important; justify-content: space-around;" class="mb-4">
                <div style="width: 40%; display: flex;">
                    <CFormInput id="from" placeholder="Min Dose" v-model="min" style="border-top-right-radius: 0; border-bottom-right-radius: 0;"/>
                    <CInputGroupText style="width: 68px; border-top-left-radius: 0; border-bottom-left-radius: 0;">mg</CInputGroupText>
                </div>
                <div style="width: 40%; display: flex;">
                    <CFormInput id="to" placeholder="Max Dose" v-model="max" style="border-top-right-radius: 0; border-bottom-right-radius: 0;"/>
                    <CInputGroupText style="width: 68px;; border-top-left-radius: 0; border-bottom-left-radius: 0;">mg</CInputGroupText>
                </div>
            </CInputGroup>
        </CModalBody>
        <CModalFooter>
            <CButton color="light" @click="onClose" style="width: 100px;">Cancel</CButton>
            <CButton color="dark" @click="onAdd" style="width: 100px;">Add</CButton>
        </CModalFooter>
    </CModal>
</template>

<script>
import { CModalBody, CModalHeader } from '@coreui/vue';


export default {
    name: 'AgeDoseModal',
    data() {
        return {
            from: "",
            to: "",
            min: "",
            max: "",
            ageType: ""
        }
    },
    props: ['modalData'],
    methods: {
        onClose() {
            this.clearModal();
            this.modalData.reject();
        },
        onAdd() {
            var response = {
                from: this.from,
                to: this.to,
                min: this.min,
                max: this.max,
                ageType: this.ageType
            };
            this.clearModal();
            this.modalData.resolve(response);
        },
        clearModal() {
            this.from = "";
            this.to = "";
            this.min = "";
            this.max = "";
        }
    },
    components: { CModalHeader, CModalBody }
}
</script>