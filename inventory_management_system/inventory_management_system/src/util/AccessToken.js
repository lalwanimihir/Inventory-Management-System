export function GetSessionData() {
  return JSON.parse(localStorage.getItem("user"));
}

export function SetSessionData(sessionData) {
  return localStorage.setItem("user", JSON.stringify(sessionData));
}

export function RemoveSessionData() {
  return localStorage.removeItem("user");
}
