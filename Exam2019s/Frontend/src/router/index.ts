import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

import AccountLogin from '../views/Account/Login.vue'
import AccountRegister from '../views/Account/Register.vue'

import AccountManageProfile from '../views/Account/ManageProfile.vue'
import AccountManageEmail from '../views/Account/ManageEmail.vue'
import AccountManagePassword from '../views/Account/ManagePassword.vue'

import Quiz from '../views/Quiz.vue'

import ChoiceCreate from '../views/Choice/Create.vue'
import ChoiceDetails from '../views/Choice/Details.vue'
import ChoiceEdit from '../views/Choice/Edit.vue'
import ChoiceDelete from '../views/Choice/Delete.vue'
import ChoiceIndex from '../views/Choice/Index.vue'

import QuestionCreate from '../views/Question/Create.vue'
import QuestionDetails from '../views/Question/Details.vue'
import QuestionEdit from '../views/Question/Edit.vue'
import QuestionDelete from '../views/Question/Delete.vue'
import QuestionIndex from '../views/Question/Index.vue'

import QuizCreate from '../views/Quiz/Create.vue'
import QuizDetails from '../views/Quiz/Details.vue'
import QuizEdit from '../views/Quiz/Edit.vue'
import QuizDelete from '../views/Quiz/Delete.vue'
import QuizIndex from '../views/Quiz/Index.vue'

import ResultCreate from '../views/Result/Create.vue'
import ResultDetails from '../views/Result/Details.vue'
import ResultEdit from '../views/Result/Edit.vue'
import ResultDelete from '../views/Result/Delete.vue'
import ResultIndex from '../views/Result/Index.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    { path: '/', name: 'Home', component: Home },

    { path: '/activeQuiz/:id?', name: 'Quiz', component: Quiz, props: true },

    { path: '/account/login', name: 'AccountLogin', component: AccountLogin },
    { path: '/account/register', name: 'AccountRegitser', component: AccountRegister },
    
    { path: '/account/manage/profile', name: 'AccountManageProfile', component: AccountManageProfile },
    { path: '/account/manage/email', name: 'AccountManageEmail', component: AccountManageEmail },
    { path: '/account/manage/password', name: 'AccountManagePassword', component: AccountManagePassword },
    
    { path: '/choice', name: 'ChoiceIndex', component: ChoiceIndex },
    { path: '/choice/create', name: 'ChoiceCreate', component: ChoiceCreate },
    { path: '/choice/edit/:id?', name: 'ChoiceEdit', component: ChoiceEdit, props: true },


    { path: '/question', name: 'QuestionIndex', component: QuestionIndex },
    { path: '/question/create', name: 'QuestionCreate', component: QuestionCreate },
    { path: '/question/edit/:id?', name: 'QuestionEdit', component: QuestionEdit, props: true },
    

    { path: '/quiz', name: 'QuizIndex', component: QuizIndex },
    { path: '/quiz/create', name: 'QuizCreate', component: QuizCreate },
    { path: '/quiz/edit/:id?', name: 'QuizEdit', component: QuizEdit, props: true },


    { path: '/result', name: 'ResultIndex', component: ResultIndex },
    { path: '/result/create', name: 'ResultCreate', component: ResultCreate },
    { path: '/result/edit/:id?', name: 'ResultEdit', component: ResultEdit, props: true },

]

const router = new VueRouter({
    routes
})

export default router
