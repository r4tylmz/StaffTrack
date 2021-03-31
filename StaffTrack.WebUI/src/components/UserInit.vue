<template>
  <div>
    <v-row class="ma-auto">
      <v-col md="12" lg="6">
        <v-card elevation="9" outlined height="100%">
          <v-card-title class="d-flex justify-center"
            >PERSONEL BILGILERI</v-card-title
          >
          <v-card>
            <v-card-title>
              <v-text-field
                v-model="searchStaff"
                append-icon="mdi-magnify"
                label="Personeller arasında ara..."
                outlined
                single-line
                hide-details
              ></v-text-field>
            </v-card-title>
            <v-data-table
              class="mx-3"
              outlined
              fixed-header
              :items-per-page="5"
              height="300"
              :headers="staffHeaders"
              :items="staffs"
              :search="searchStaff"
            >
              <template v-slot:top>
                <v-toolbar flat>
                  <v-spacer></v-spacer>
                  <v-dialog v-model="dialog" max-width="500px">
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn
                        color="deep-purple"
                        dark
                        class="mb-2"
                        v-bind="attrs"
                        v-on="on"
                      >
                        PERSONEL EKLE
                      </v-btn>
                    </template>
                    <v-card>
                      <v-card-title>
                        <span class="headline">{{ formTitle }}</span>
                      </v-card-title>

                      <v-card-text>
                        <v-container>
                          <v-row>
                            <v-col cols="12" sm="6" md="4">
                              <v-text-field
                                v-model="editedItem.name"
                                label="Ad"
                              ></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6" md="4">
                              <v-text-field
                                v-model="editedItem.lastName"
                                label="Soyad"
                              ></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6" md="4">
                              <v-text-field
                                v-model="editedItem.phoneNumber"
                                label="Telefon"
                              ></v-text-field>
                            </v-col>
                          </v-row>
                        </v-container>
                      </v-card-text>

                      <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" text @click="close">
                          IPTAL
                        </v-btn>
                        <v-btn color="success" @click="save"> KAYDET </v-btn>
                      </v-card-actions>
                    </v-card>
                  </v-dialog>
                  <v-dialog v-model="dialogDelete" max-width="500px">
                    <v-card>
                      <v-card-title class="headline"
                        >Silmek istediginize emin misiniz?</v-card-title
                      >
                      <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" dark @click="closeDelete"
                          >IPTAL</v-btn
                        >
                        <v-btn color="error" dark @click="deleteItemConfirm"
                          >SIL</v-btn
                        >
                        <v-spacer></v-spacer>
                      </v-card-actions>
                    </v-card>
                  </v-dialog>
                </v-toolbar>
              </template>
              <template v-slot:[`item.actions`]="{ item }">
                <v-icon
                  medium
                  color="primary"
                  class="mr-2"
                  @click="userDetail(item)"
                >
                  mdi-magnify
                </v-icon>
                <v-icon
                  medium
                  color="success"
                  class="mr-2"
                  @click="editItem(item)"
                >
                  mdi-pencil
                </v-icon>
                <v-icon medium color="error" @click="deleteItem(item)">
                  mdi-delete
                </v-icon>
              </template>
            </v-data-table>
          </v-card>
        </v-card>
      </v-col>
      <v-col md="12" lg="6">
        <v-card elevation="9" outlined height="100%">
          <v-card>
            <v-card-title class="d-flex justify-center"
              >PERSONELİN GİRİŞ ÇIKIŞLARI</v-card-title
            >
            <v-card-title>
              <v-text-field
                v-model="searchActivities"
                append-icon="mdi-magnify"
                label="Personel aktiviteleri arasında ara..."
                outlined
                single-line
                hide-details
              ></v-text-field>
            </v-card-title>
            <v-data-table
              class="mx-3"
              height="365"
              outlined
              :items-per-page="5"
              fixed-header
              :headers="activityHeaders"
              :items="staffActivities"
              :search="searchActivities"
            >
            </v-data-table>
          </v-card>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      searchStaff: "",
      staffHeaders: [
        { text: "ID", value: "id", align: "start", filterable: true },
        { text: "AD", value: "name" },
        { text: "SOYAD", value: "lastName" },
        { text: "TELEFON NO", value: "phoneNumber" },
        { text: "ISLEM", value: "actions" },
      ],
      staffs: [],
      searchActivities: "",
      activityHeaders: [
        { text: "PERSONEL ID", value: "staffId", align: "start", filterable: true,},
        { text: "ODA ID", value: "roomId" },
        { text: "AD", value: "name" },
        { text: "SOYAD", value: "lastName" },
        { text: "GİRİŞ", value: "entryTime" },
        { text: "ÇIKIŞ", value: "exitTime" },
      ],
      staffActivities: [],
      dialog: false,
      dialogDelete: false,
      editedIndex: -1,
      editedItem: {
        id: -1,
        name: "",
        lastName: "",
        phoneNumber: "",
      },
      defaultItem: {
        id: -1,
        name: "",
        lastName: "",
        phoneNumber: "",
      },
    };
  },
  created() {
    axios
      .get("https://localhost:5001/api/Staff")
      .then((response) => this.staffs.push(...response.data));

    axios
      .get("https://localhost:5001/api/StaffActivity")
      .then((response) => this.staffActivities.push(...response.data));
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Personel Ekle" : "Personel Duzenle";
    },
  },
  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },
  methods: {
    editItem(item) {
      this.editedIndex = this.staffs.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.editedItem.id = this.staffs[this.editedIndex].id;
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.staffs.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.editedItem.id = this.staffs[this.editedIndex].id;
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      axios
        .delete("https://localhost:5001/api/Staff/id", {
          params: { id: this.editedItem.id },
        })
        .then((response) => console.log(response));
      this.staffs.splice(this.editedIndex, 1);
      this.closeDelete();
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    userDetail(item) {
      this.editedIndex = this.staffs.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.editedItem.id = this.staffs[this.editedIndex].id;
      this.$router.push(`users/${this.editedItem.id}`)
    },

    save() {
      if (this.editedIndex > -1) {
        axios
          .put("https://localhost:5001/api/Staff", {
            id: this.editedItem.id,
            name: this.editedItem.name,
            lastName: this.editedItem.lastName,
            phoneNumber: this.editedItem.phoneNumber,
          })
          .then((response) => console.log(response));

        Object.assign(this.staffs[this.editedIndex], this.editedItem);
      } else {
        axios
          .post("https://localhost:5001/api/Staff", {
            name: this.editedItem.name,
            lastName: this.editedItem.lastName,
            phoneNumber: this.editedItem.phoneNumber,
          })
          .then((response) => this.staffs.push(response.data));
      }
      this.close();
    },
  },
};
</script>

<style>
</style>