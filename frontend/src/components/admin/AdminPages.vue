<template>
  <div class="admin-pages">
    <PageTitle icon="fa fa-cogs" main="Administration" sub="Desafio Lin" />
    <div class="admin-pages-tabs">
      <b-card no-body>
        <b-tabs card>
          <b-tab title="Users">
            <UserAdmin />
          </b-tab>
          <b-tab title="Authorization Test">
            <AuthorizationTest />
          </b-tab>
        </b-tabs>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageTitle from "../template/PageTitle";
import UserAdmin from "./UserAdmin";
import AuthorizationTest from "./AuthorizationTest";

export default {
  name: "AdminPages",
  components: { PageTitle, UserAdmin, AuthorizationTest },
};
</script>

<style>
</style>
