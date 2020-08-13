<template>
  <div class="app">
    <v-data-table :headers="headers" :items="movies" :items-per-page="5" class="elevation-1">
      <template v-slot:top>
        <v-dialog v-model="dialog" width="500">
          <template v-slot:activator="{on}">
            <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
          </template>
          <v-card>
            <v-card-title class="headline grey lighten-2">Privacy Policy</v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.name" label="Name"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.Description" label="Description"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.ReleaseYear" label="Release Year"></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="save()">Save</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon small @click="edit(item)">mdi-pencil</v-icon>
        <v-icon small @click="remove(item)">mdi-delete</v-icon>
      </template>
    </v-data-table>

    <div v-for="movie in movies" :key="movie.id">{{ movie.email }}</div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      headers: [
               {
          text: "email",
          align: "start",
          value: "email",
        },
        {
          text: "name",
          align: "start",
          value: "name",
        },
        { text: "Description", value: "Description" },
        { text: "ReleaseYear", value: "ReleaseYear" },
        { text: "Actions", value: "actions", sortable: false },

        //   { text: 'Protein (g)', value: 'protein' },
        //   { text: 'Iron (%)', value: 'iron' },
      ],
      movies: [],
      dialog: false,
      editedItem: {
        name: "",
        Description: "",
        ReleaseYear: "",
      },
      defaultItem: {
        name: "",
        Description: "",
        ReleaseYear: "",
      },
    };
  },

  created() {
    this.initialize();
  },
  methods: {
    initialize() {
      axios
        .get("https://reqres.in/api/users?page=2")
        .then((res) => {
          this.movies = res.data.data.map((item) => {
            return item;
          });
        })
        .catch((err) => console.log(err));
    },
    save() {
      if (this.movies.some(({ id }) => this.editedItem.id === id)) {
        this.movies = this.movies.map((movie) =>
          movie.id === this.editedItem.id ? this.editedItem : movie
        );
      } else {
        this.movies.push(...this.movies, {
          ...this.editedItem,
          id: Date.now(),
        }); // change Date.now
      }
      // add a post request and add push afterwards
      this.close();
    },
    update() {},
    edit(item) {
      this.editedItem = { ...item };
      this.dialog = true;
    },
    remove(item) {
      this.movies = this.movies.filter((movie) => {
        return movie.id !== item.id;
      });
    },
    close() {
      this.editedItem = { ...this.defaultItem };
      this.dialog = false;
    },
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
