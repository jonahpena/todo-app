// tasks.js
const API_URL = process.env.REACT_APP_API_URL;

export const fetchTasks = () => {
  return fetch(`${API_URL}`)
    .then((response) => response.json())
    .then((data) => {
      const uncompletedTasks = data.filter((task) => !task.completed);
      const completedTasks = data.filter((task) => task.completed);
      return { uncompletedTasks, completedTasks };
    });
};

export const addTask = (title) => {
  return fetch(`${API_URL}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ title }),
  }).then((response) => response.json());
};

export const deleteTask = (id) => {
  return fetch(`${API_URL}/${id}`, { method: "DELETE" });
};

export const updateTask = (id, updatedTask) => {
  return fetch(`${API_URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(updatedTask),
  });
};
