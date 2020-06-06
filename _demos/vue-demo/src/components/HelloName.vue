<template>
    <div>Hello, {{ msg }}!</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

@Component
export default class HelloName extends Vue {
    @Prop()
    private msg!: string;
}
</script>

<style>

</style>
