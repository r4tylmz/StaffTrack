<template>
  <div>
    <v-row class="ma-auto">
      <v-col md="12" lg="4">
        <v-card class="elevation-4" height="100%">
          <v-row class="mt-0">
            <v-col class="d-flex justify-center" md="12" lg="12">
              <v-avatar class="profile" color="grey" size="300">
                <v-img src="https://via.placeholder.com/400.jpg"></v-img>
              </v-avatar>
            </v-col>
            <v-col md="12" lg="12">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="d-flex justify-center display-3">
                    {{ toUpper(this.staff.name + " " + this.staff.lastName) }}
                  </v-list-item-title>
                  <v-list-item-subtitle class="d-flex justify-center display-1">
                    {{ this.staff.phoneNumber }}
                  </v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-col>
          </v-row>
        </v-card>
      </v-col>
      <v-col md="12" lg="8">
        <v-card class="elevation-4">
          <v-card-title class="d-flex justify-center">PERSONELİN İŞ SADAKATİ</v-card-title>
          <div id="chartdiv"></div>
        </v-card>
      </v-col>
      <v-col md="12" lg="12">
        <v-card class="elevation-4">
          <v-card-title class="d-flex justify-center"
            >PERSONELİN GİRİŞ ÇIKIŞLARI</v-card-title
          >
          <v-card>
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
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

export default {
  data() {
    return {
      staff: {
        id: 0,
        name: "",
        lastName: "",
        phoneNumber: "",
      },
      activityHeaders: [
        {
          text: "PERSONEL ID",
          value: "staffId",
          align: "start",
          filterable: true,
        },
        { text: "ODA ID", value: "roomId", filterable: true },
        { text: "AD", value: "name" },
        { text: "SOYAD", value: "lastName" },
        { text: "GİRİŞ", value: "entryTime" },
        { text: "ÇIKIŞ", value: "exitTime" },
      ],
      staffActivities: [],
      searchActivities: "",
    };
  },
  methods: {
    toUpper(string) {
      return string.toUpperCase();
    },
  },
  created() {
    axios
      .get(`https://localhost:5001/api/Staff/${this.$route.params.id}`)
      .then((response) => {
        this.staff = Object.assign({}, response.data);
      });
    axios
      .get(`https://localhost:5001/api/StaffActivity/${this.$route.params.id}`)
      .then((response) => this.staffActivities.push(...response.data));
  },
  mounted() {
    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    // Create chart instance
    var chart = am4core.create("chartdiv", am4charts.PieChart);
    axios
      .get(
        `https://localhost:5001/api/StaffActivity/${this.$route.params.id}/loyality`
      )
      .then((response) => {
        chart.data = [
          {
            Zaman: "Toplam Zaman",
            Sure: response.data,
          },
          {
            Zaman: "Gereken Zaman",
            Sure: 600,
          },
        ];
      });

    // Add and configure Series
    var pieSeries = chart.series.push(new am4charts.PieSeries());
    pieSeries.dataFields.value = "Sure";
    pieSeries.dataFields.category = "Zaman";
    pieSeries.slices.template.stroke = am4core.color("#fff");
    pieSeries.slices.template.strokeWidth = 2;
    pieSeries.slices.template.strokeOpacity = 1;

    // This creates initial animation
    pieSeries.hiddenState.properties.opacity = 1;
    pieSeries.hiddenState.properties.endAngle = -90;
    pieSeries.hiddenState.properties.startAngle = -90;
    chart.hiddenState.properties.radius = am4core.percent(0);
  },
};
</script>

<style scoped>
#chartdiv {
  width: 100%;
  height: 500px;
}
</style>
