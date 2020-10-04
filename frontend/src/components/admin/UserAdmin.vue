<template>
  <div class="user-admin">
    <b-form>
      <input id="user-id" type="hidden" v-model="user.id" />
      <b-row>
        <b-col md="6" sm="12">
          <b-form-group label="Login:" label-for="user-login">
            <b-form-input
              id="user-login"
              type="text"
              v-model="user.login"
              required
              :readonly="mode === 'remove'"
              placeholder="Your e-mail..."
              autocomplete="off"
            />
          </b-form-group>
        </b-col>
      </b-row>
      <b-row v-show="mode === 'save'">
        <b-col md="6" sm="12">
          <b-form-group label="Password:" label-for="user-password">
            <b-form-input
              id="user-password"
              type="password"
              v-model="user.password"
              required
              placeholder="Your password..."
            />
          </b-form-group>
        </b-col>
      </b-row>
      <b-table
        small
        hover
        striped
        :items="user.authorizations"
        :fields="authFields"
      >
        <template slot="actions" slot-scope="data">
          <b-form-select
            v-model="data.item.value"
            :options="options"
          ></b-form-select>
        </template>
      </b-table>
      <b-row>
        <b-col xs="12">
          <b-button variant="primary" v-if="mode === 'save'" @click="save"
            >Save</b-button
          >
          <b-button variant="danger" v-if="mode === 'remove'" @click="remove"
            >Remove</b-button
          >
          <b-button class="ml-2" @click="reset">Cancelar</b-button>
        </b-col>
      </b-row>
    </b-form>
    <hr />
    <b-table hover striped :items="users" :fields="fields">
      <template slot="actions" slot-scope="data">
        <b-button variant="success" @click="loadUser(data.item)" class="mr-2">
          <i class="fa fa-eye"></i>
        </b-button>
      </template>
    </b-table>
  </div>
</template>

<script>
import { baseApiUrl, showError } from "@/global";
import axios from "axios";

export default {
  name: "UserAdmin",
  data: function () {
    return {
      mode: "save",
      user: {},
      users: [],
      fields: [
        { key: "id", label: "Id", sortable: true },
        { key: "login", label: "Login", sortable: true },
        { key: "actions", label: "Actions" },
      ],
      authFields: [
        { key: "name", label: "Authorization", sortable: true },
        { key: "actions", label: "Authorized", sortable: true },
      ],
      options: [
        { text: "Yes", value: true },
        { text: "No", value: false },
      ],
    };
  },
  methods: {
    loadUsers() {
      const url = `${baseApiUrl}user`;
      axios.get(url).then((res) => {
        this.users = res.data;
      });
    },
    reset() {
      this.mode = "save";
      this.user = {};
      this.loadUsers();
    },
    save() {
      const method = this.user.id ? "put" : "post";
      axios[method](`${baseApiUrl}user`, this.user)
        .then(() => {
          this.$toasted.global.defaultSuccess();
          this.reset();
        })
        .catch(showError);
    },
    remove() {
      const id = this.user.id;
      axios
        .delete(`${baseApiUrl}/users/${id}`)
        .then(() => {
          this.$toasted.global.defaultSuccess();
          this.reset();
        })
        .catch(showError);
    },
    loadUser(user, mode = "save") {
      this.mode = mode;
      this.user = { ...user };
    },
  },
  mounted() {
    this.loadUsers();
  },
};
</script>

<style>
</style>
