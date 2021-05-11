import {store} from '../store/state'

function getHeader() {
  if (store.state.isLoggedIn) {
    return "Basic " + btoa(store.state.username + ":" + store.state.password)
  }
  return "";
}

export function get(url) {
  return new Promise((resolve, reject) => {
    // eslint-disable-next-line no-undef
    $.ajax({
      type: 'GET',
      dataType: 'text',
      url: "https://localhost:5001/" + url,
      headers: {
        "Authorization": getHeader(),
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      success: function (responseData, textStatus, jqXHR) {
        try {
          resolve(JSON.parse(responseData), textStatus, jqXHR);
        } catch (e) {
          resolve(responseData, textStatus, jqXHR);
        }
      },
      error: function (responseData, textStatus, errorThrown) {
        try {
          reject(JSON.parse(responseData), textStatus, errorThrown);
        } catch (e) {
          reject(responseData, textStatus, errorThrown);
        }
      }
    })
  })
}

export function post(url, data) {
  return new Promise((resolve, reject) => {
    // eslint-disable-next-line no-undef
    $.ajax({
      type: 'POST',
      crossDomain: true,
      data: data ? JSON.stringify(data) : "{}",
      dataType: 'text',
      url: "https://localhost:5001/" + url,
      headers: {
        "Authorization": getHeader(),
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      success: function (responseData, textStatus, jqXHR) {
        try {
          resolve(JSON.parse(responseData), textStatus, jqXHR);
        } catch (e) {
          resolve(responseData, textStatus, jqXHR);
        }
      },
      error: function (responseData, textStatus, errorThrown) {
        try {
          reject(JSON.parse(responseData), textStatus, errorThrown);
        } catch (e) {
          reject(responseData, textStatus, errorThrown);
        }
      }
    })
  })
}
