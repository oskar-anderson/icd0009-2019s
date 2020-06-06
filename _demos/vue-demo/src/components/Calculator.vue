<template>
    <div class="section" id="calculator">

      <!--   display  -->
      <div class="row">
        <div class="col s12 m12 right-align brown darken-4">
          <h1 class="white-text js-display">-</h1>
        </div>
      </div>

      <!-- row 1 -->
      <div class="row">
        <div class="col s3 m3">
          <a @click="clickAC($event)" class="waves-effect waves-light btn brown">AC</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn brown">+/-</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn brown">%</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn orange">/</a>
        </div>
      </div>
      <!-- row 1 -->
      <div class="row">
        <div class="col s3 m3">
          <a @click="numberClicked(7)" class="waves-effect waves-light btn grey js-number" data-value="7">seven</a>
        </div>
        <div class="col s3 m3">
          <a @click="numberClicked(8)" class="waves-effect waves-light btn grey js-number" data-value="8">eight</a>
        </div>
        <div class="col s3 m3">
          <a @click="numberClicked(9)" class="waves-effect waves-light btn grey js-number" data-value="9">nine</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn orange">*</a>
        </div>
      </div> <!-- row 1 -->
      <div class="row">
        <div class="col s3 m3">
          <a @click="numberClicked(4)" class="waves-effect waves-light btn grey">4</a>
        </div>
        <div class="col s3 m3">
          <a @click="numberClicked(5)" class="waves-effect waves-light btn grey">5</a>
        </div>
        <div class="col s3 m3">
          <a @click="numberClicked(6)" class="waves-effect waves-light btn grey">6</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn orange">-</a>
        </div>
      </div> <!-- row 1 -->
      <div class="row">
        <div class="col s3 m3">
          <a @click="numberClicked(1)" class="waves-effect waves-light btn grey">1</a>
        </div>
        <div class="col s3 m3">
          <a @click="numberClicked(2)" class="waves-effect waves-light btn grey">2</a>
        </div>
        <div class="col s3 m3">
          <a @click="numberClicked(3)" class="waves-effect waves-light btn grey">3</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn orange">+</a>
        </div>
      </div> <!-- row 1 -->
      <div class="row">
        <div class="col s6 m6">
          <a class="waves-effect waves-light btn grey">0</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn grey">,</a>
        </div>
        <div class="col s3 m3">
          <a class="waves-effect waves-light btn orange">=</a>
        </div>
      </div>

    <div>
        <div class="col s6 m6">
            <input v-model="numberAsText" @change="textUpdated" type="text">
        </div>
    </div>

    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Provide } from 'vue-property-decorator'
@Component
export default class Calculator extends Vue {
    @Provide()
    display = '0'

    @Provide()
    numberAsText = '123';

    clickAC (event: Event): void {
      console.log(event)
    }

    numberClicked (num: number): void {
      this.display = this.display + num.toString()
    }

    textUpdated (): void {
      this.display = Number(this.numberAsText).toString()
      console.log(this.numberAsText)
    }
}
</script>
<style scoped>
</style>>
