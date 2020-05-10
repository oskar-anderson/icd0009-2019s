<template>
    <div>
        <h1>GpsSessions Index</h1>
        <ul>
            <li v-for="session in sessions" :key="session.id">
                {{session.userFirstLastName}} - {{session.name}} - {{session.gpsLocationsCount}}
                <button
                    v-if="session.gpsLocationsCount < 50"
                    @click="deleteOnClick(session)"
                    type="button"
                    class="btn btn-danger"
                >Delete</button>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { IGpsSession } from "../../domain/IGpsSession";
import store from "../../store";

@Component
export default class GpsSessionsIndex extends Vue {
    get sessions(): IGpsSession[] {
        return store.state.gpsSessions;
    }

    deleteOnClick(session: IGpsSession): void {
        if (session.gpsLocationsCount < 50) {
            store.dispatch('deleteGpsSession', session.id);
        }
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        console.log("mounted");
        store.dispatch("getGpsSessions");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>
