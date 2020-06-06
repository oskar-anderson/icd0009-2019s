import Vue from 'vue'
import Vuex, { Store } from 'vuex'
import { ILoginDTO } from '@/types/ILoginDTO';
import { AccountApi } from '@/services/AccountApi';
import { GpsSessionsApi } from '@/services/GpsSessionApi';
import { IGpsSession } from './../domain/IGpsSession';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        gpsSessions: [] as IGpsSession[]
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },
        setGpsSessions(state, gpsSessions: IGpsSession[]) {
            state.gpsSessions = gpsSessions;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        }
    },
    actions: {
        clearJwt(context): void {
            context.commit('setJwt', null);
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async getGpsSessions(context): Promise<void> {
            const gpsSessions = await GpsSessionsApi.getAll();
            context.commit('setGpsSessions', gpsSessions);
        },
        async deleteGpsSession(context, id: string): Promise<void> {
            console.log('deleteGpsSession', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await GpsSessionsApi.delete(id, context.state.jwt);
                await context.dispatch('getGpsSessions');
            }
        }
    },
    modules: {
    }
})
