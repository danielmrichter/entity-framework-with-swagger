import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
// import registerServiceWorker from './registerServiceWorker';
import { Provider } from 'react-redux';
import { createStore, combineReducers } from 'redux';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const bakersReducer = (state = [], action) => {
  if (action.type === 'SET_BAKERS')
    return action.payload;
  return state;
}

const breadReducer = (state = [], action) => {
  if (action.type === 'SET_BREAD')
    return action.payload;
  return state;
}

const reduxStore = createStore(
  combineReducers({
    bakers: bakersReducer,
    breadInventory: breadReducer
  })
);
window.store = reduxStore;

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <Provider store={reduxStore}>
      <App />
    </Provider>
  </BrowserRouter>,
  rootElement);

// registerServiceWorker();

