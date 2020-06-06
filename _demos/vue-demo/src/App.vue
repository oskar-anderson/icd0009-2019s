<template>
  <div class="container">
    <Calculator />
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Provide } from 'vue-property-decorator'
import Calculator from './components/Calculator.vue'

@Component({
  components: {
    Calculator
  }
})

/*
import HelloWorld from './components/HelloWorld.vue'

@Component({
  components: {
    HelloWorld
  }
})
*/

export default class App extends Vue {
@Provide()
  private name = 'Andres';
}
</script>

<style>

</style>
