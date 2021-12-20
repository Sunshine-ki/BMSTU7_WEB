import React from 'react';

import './App.css';

import Register from "./components/Register/Register";

import {
    Routes,
    Route,
    Link
} from "react-router-dom";

import { Helmet } from "react-helmet";


import Login from "./components/Login/Login";

import Stats from "./components/Stats/Stats"
import TaskItem from "./components/TaskList/TaskItem/TaskItem"
import TaskList from "./components/TaskList/TaskList"
import Task from "./components/Task/Task";

function App() {
  return (
      <div className="App">

          <div className="container flex mx-auto ">
              <nav className="flex w-full md:w-1/2 mx-auto bg-indigo-700 h-12 items-center justify-around mb-8">
                  <Link to="Tasks" className="text-white hover:text-indigo-200 transition-all">Задачи</Link>
                  <Link to="stats" className="text-white hover:text-indigo-200 transition-all">Статистика</Link>
              </nav>
          </div>

          <Routes>
              <Route path="tasks" element={(
                  <>
                      <Helmet>
                          <title>Список задании</title>
                      </Helmet>
                      <TaskList items={[{
                          "id": 0,
                          "name": "find",
                          "shortDescription": "Find all rows",
                          "detailedDescription": "Find all rows from test table",
                          "solution": "",
                          "tableName": "test",
                          done: true,
                          "authorId": 126
                      },{
                          "id": 1,
                          "name": "find 2",
                          "shortDescription": "Find all rows 2",
                          "detailedDescription": "Find all rows from test table",
                          "solution": "",
                          "tableName": "test",
                          done: false,
                          "authorId": 126
                      },{
                          "id": 2,
                          "name": "find 3",
                          "shortDescription": "Find all rows 3",
                          "detailedDescription": "Find all rows from test table",
                          "solution": "",
                          "tableName": "test",
                          done: true,
                          "authorId": 126
                      }]} />
                  </>
              )} />
              <Route path="login" element={(
                  <>
                      <Helmet>
                          <title>Вход</title>
                      </Helmet>
                      <Login />
                  </>
              )} />
              <Route path="stats" element={(
                  <>
                      <Helmet>
                          <title>Статистика</title>
                      </Helmet>
                      <Stats />
                  </>
              )} />
              <Route path="task/:id" element={<Task />} />
              <Route path="register" element={(
                  <>
                      <Helmet>
                          <title>Регистрация</title>
                      </Helmet>
                      <Register />
                  </>
              )} />
          </Routes>
     </div>
  );
}


export default App;
