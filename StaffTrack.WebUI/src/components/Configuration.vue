<template>
  <v-app>
    <v-container fluid>
      <v-main class="ma-10">
        <v-row>
          <v-col cols="12">
            <v-card elevation="9" outlined height="100%">
              <v-card-title class="d-flex justify-center">
                Mesai Ayarları
              </v-card-title>
              <v-row class="ma-5">
                <v-col cols="6">
                  <v-menu
                      ref="workingTimeStart"
                      v-model="workingTimeStart"
                      :close-on-content-click="false"
                      :nudge-right="40"
                      :return-value.sync="timeStart"
                      transition="scale-transition"
                      offset-y
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                          v-model="timeStart"
                          label="Mesai Başlangıç"
                          prepend-icon="mdi-clock-time-four-outline"
                          readonly
                          v-bind="attrs"
                          v-on="on"
                      ></v-text-field>
                    </template>
                    <v-time-picker
                        v-if="workingTimeStart"
                        v-model="timeStart"
                        format="24hr"
                        header-color="deep-purple"
                        full-width
                        landscape
                        @click:minute="$refs.workingTimeStart.save(timeStart)"
                    ></v-time-picker>
                  </v-menu>

                </v-col>
                <v-col cols="6">
                  <v-menu
                      ref="workingTimeEnd"
                      v-model="workingTimeEnd"
                      :close-on-content-click="false"
                      :nudge-right="40"
                      :return-value.sync="timeEnd"
                      transition="scale-transition"
                      offset-y
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                          v-model="timeEnd"
                          label="Mesai Bitiş"
                          prepend-icon="mdi-clock-time-four-outline"
                          readonly
                          v-bind="attrs"
                          v-on="on"
                      ></v-text-field>
                    </template>
                    <v-time-picker
                        v-if="workingTimeEnd"
                        v-model="timeEnd"
                        format="24hr"
                        full-width
                        header-color="deep-purple"
                        landscape
                        @click:minute="$refs.workingTimeEnd.save(timeEnd)"
                    ></v-time-picker>
                  </v-menu>

                </v-col>
                <v-col cols="6">
                  <v-text-field
                      v-model="wagePerHour"
                      label="Personelin Saatlik Ücreti"
                      placeholder="Saatlik Ücret"
                      prepend-icon="mdi-currency-try"
                  ></v-text-field>
                </v-col>
                <v-col cols="6">
                  <v-text-field
                      v-model="totalWorkingTime"
                      label="Haftalık Toplam Mesai Saati"
                      placeholder="Haftalık Toplam Mesai Saati"
                      prepend-icon="mdi-clock-time-four"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-btn
                  dark
                  class="ma-2"
                  color="deep-purple"
                  @click="send"
              >Kaydet
              </v-btn>
            </v-card>
          </v-col>
        </v-row>
      </v-main>
    </v-container>
  </v-app>
</template>

<script>

import axios from "axios";
import swal from 'sweetalert';

export default {
  data() {
    return {
      workingTimeStart: '',
      workingTimeEnd: '',
      wagePerHour: 0,
      totalWorkingTime: 0,
      timeStart: null,
      timeEnd: null,

    }
  },
  created() {
    axios
        .get("https://localhost:5001/api/constant")
        .then((response) => {
          this.timeStart = response.data.workingHourStart
          this.timeEnd = response.data.workingHourEnd
          this.wagePerHour  = response.data.hourlyWage
          this.totalWorkingTime  = response.data.workingHourPerWeek
        });
  },
  methods: {
    send() {
      axios
        .put("https://localhost:5001/api/constant",{
          id: 1,
          workingHourStart: this.timeStart,
          workingHourEnd: this.timeEnd,
          hourlyWage:  this.wagePerHour,
          workingHourPerWeek: this.totalWorkingTime
        }).then(()=>{
        swal("Başarılı", "Başarıyla kaydedildi!", "success");
      })
    }
  }
}
</script>

<style scoped>

</style>