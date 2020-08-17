<template>
  <div class="app">
    <v-data-table :headers="headers" :items="movies" :items-per-page="all" class="elevation-1">
      <template v-slot:top>
        <v-dialog v-model="dialog" width="500">
          <template v-slot:activator="{on}">
            <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
          </template>
          <v-card>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.name" :counter="max" :rules="nameRules" label="Name" required></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.description" label="description"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.releaseYear" :rules="releaseYearRules" label="Release Year" required></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="save()">Save</v-btn>
              <v-btn color="primary" text @click="close()">Cancel</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon small @click="edit(item)">mdi-pencil</v-icon>
        <v-icon small @click="copy(item)">mdi-checkbox-multiple-blank-outline</v-icon>
        <v-icon small @click="remove(item)">mdi-delete</v-icon>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data: () => ({
valid: true,
      name: '',
      nameRules: [
        v => !!v || 'Name is required',
        v => (v && v.length <= 50) || 'Name must be less than 50 characters',
      ],
      description: '',
      descriptionRules: [
        v => !!v || 'Release Year is required',
        v => (v && v.length <= 500) || 'Release Year must be less than 500 characters',
      ],
      releaseYear: '',
      releaseYearRules: [
        v => !!v || 'Release Year is required',
        v => (v && v.length === 4) || 'Release Year must be 4 characters',
      ],
    
      headers: [
        {
          text: "id",
          align: "start",
          value: "id",
        },
        {
          text: "name",
          align: "start",
          value: "name",
        },
        {
          text: "description",
          align: "start",
          value: "description",
        },
        { text: "releaseYear", value: "releaseYear" },
        { text: "Actions", value: "actions", sortable: false },
      ],
      movies: [],
      dialog: false,
      editedItem: {
        name: "",
        description: "",
        releaseYear: null,
      },
      defaultItem: {
        name: "",
        description: "",
        releaseYear: null,
      },
}),
  created() {
    this.initialize();
  },
  methods: {
    initialize() {
      axios
        .get("http://localhost:5000/api/Movie")
        .then((res) => {
          console.log(res);
          this.movies = res.data.map((item) => {
            console.log(item.id);
            return item;
          });
        })
        .catch((err) => console.log(err));
    },
    save() {
      if (this.editedItem.id) {
        axios
          .put(
            `http://localhost:5000/api/Movie/${this.editedItem.id}`,
            this.editedItem
          )
          .then(() => {
            axios.get("http://localhost:5000/api/Movie").then((res) => {
              this.movies = res.data.map((item) => {
                return item;
              });
            });
          })
          .catch((err) => console.log(err));
      } else {
        axios
          .post("http://localhost:5000/api/Movie", this.editedItem)
          .then(() => {
            axios
              .get("http://localhost:5000/api/Movie")
              .then((res) => {
                this.movies = res.data.map((item) => {
                  return item;
                });
              })
              .catch((err) => console.log(err));
          });
      }
      this.close();
    },
    edit(item) {
      this.editedItem = { ...item };
      this.dialog = true;
    },
    copy(item) {
      let newItem = { ...item };
      delete newItem.id;
      axios.post("http://localhost:5000/api/Movie", newItem).then(() => {
        axios
          .get("http://localhost:5000/api/Movie")
          .then((res) => {
            this.movies = res.data.map((item) => {
              return item;
            });
          })
          .catch((err) => console.log(err));
      });
    },
    remove(item) {
      axios
        .delete(`http://localhost:5000/api/Movie/${item.id}`)
        .then(() => {
          axios.get("http://localhost:5000/api/Movie").then((res) => {
            this.movies = res.data.filter((movie) => {
              return movie.id !== item.id;
            });
          });
        })
        .catch((err) => console.log(err));
    },
    close() {
      this.editedItem = { ...this.defaultItem };
      this.dialog = false;
    },
    validateField() {
      this.$refs.form.validate();
    },
  },
  computed: {
    rules() {
      const rules = [];

      if (this.max) {
        const rule = (v) =>
          (v || "").length <= this.max ||
          `A maximum of ${this.max} characters is allowed`;

        rules.push(rule);
      }

      if (!this.allowSpaces) {
        const rule = (v) =>
          (v || "").indexOf(" ") < 0 || "No spaces are allowed";

        rules.push(rule);
      }

      if (this.match) {
        const rule = (v) => (!!v && v) === this.match || "Values do not match";

        rules.push(rule);
      }

      return rules;
    },
  },
  watch: {
    match: "validateField",
    max: "validateField",
    model: "validateField",
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>
