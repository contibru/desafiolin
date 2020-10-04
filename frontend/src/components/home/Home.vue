<template>
  <div class="home">
    <PageTitle icon="fa fa-home" main="Dashboard" sub="Desafio Lin" />
  </div>
</template>

<script>
import PageTitle from "../template/PageTitle";

export default {
  name: "Home",
  components: { PageTitle },
  data: function () {
    return {};
  },
  methods: {},
  mounted() {},
};
</script>

<style>
.stats {
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
}
</style>
