import React, { useState } from "react";
import "./TaskList.css";

function AddTaskItemForm({ onAddItem }) {
  const [inputValue, setInputValue] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (inputValue.trim()) {
      onAddItem(inputValue);
      setInputValue("");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="row">
        <div className="input-field col s8">
          <input
            type="text"
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
            id="todo-input"
            className="validate"
          />
          <label htmlFor="todo-input">Enter your task</label>
        </div>
        <div>
          <button
            onClick={handleSubmit}
            id="addTaskButton"
            className="waves-effect"
          >
            Add
          </button>
        </div>
      </div>
    </form>
  );
}

export default AddTaskItemForm;
