import React, { useState, useEffect } from "react";
import "./TaskList.css";
import ListItem from "./ListItem";
import CompletedListItem from "./CompletedListItem";

const API_URL = process.env.REACT_APP_API_URL;

function TaskList() {
  const [items, setItems] = useState([]);
  const [completedItems, setCompletedItems] = useState([]);
  const [inputValue, setInputValue] = useState("");
  const [invalidInput, setInvalidInput] = useState(false);
  const [showCompletedItems, setShowCompletedItems] = useState(false);

  useEffect(() => {
    fetch(`${API_URL}`)
      .then((response) => response.json())
      .then((data) => setItems(data));
  }, []);

  const handleAddItem = () => {
    if (inputValue.trim()) {
      // POST the new task to the API
      fetch(`${API_URL}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ title: inputValue }),
      })
        .then((response) => response.json())
        .then((data) => setItems([data, ...items]));

      setInputValue("");
      setInvalidInput(false);
    } else {
      setInvalidInput(true);
    }
  };

  const handleRemoveItem = (id) => {
    // DELETE the task from the API
    fetch(`${API_URL}/${id}`, { method: "DELETE" }).then(() =>
      setItems(items.filter((item) => item.id !== id))
    );
  };

  const handleCompleteItem = (item) => {
    // PUT the task to the API with the completed state
    fetch(`${API_URL}/${item.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...item, isComplete: true }),
    }).then(() => {
      setItems(items.filter((i) => i.id !== item.id));
      setCompletedItems([item, ...completedItems]);
    });
  };

  const handleMoveBackToTask = (item) => {
    // PUT the task to the API with the uncompleted state
    fetch(`${API_URL}/${item.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...item, isComplete: false }),
    }).then(() => {
      setCompletedItems(completedItems.filter((i) => i.id !== item.id));
      setItems([item, ...items]);
    });
  };

  const hasCompletedItems = completedItems.length > 0;

  return (
    <div className="container">
      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleAddItem();
        }}
      >
        <div className="row">
          <div
            className={`input-field col s8 ${invalidInput ? "invalid" : ""}`}
          >
            <input
              type="text"
              value={inputValue}
              onChange={(e) => {
                setInputValue(e.target.value);
                setInvalidInput(false);
              }}
              id="todo-input"
              className="validate"
            />
            <label htmlFor="todo-input">Enter your task</label>

            <span className="helper-text" data-error="Please enter a task" />
            {invalidInput && (
              <span className="error-message">Please enter a valid task</span>
            )}
          </div>
        </div>
      </form>

      <ul>
        {items.reverse().map((item, index) => (
          <ListItem
            key={index}
            item={item}
            index={index}
            handleRemoveItem={handleRemoveItem}
            handleCompleteItem={handleCompleteItem}
          />
        ))}
      </ul>

      {hasCompletedItems && (
        <div className="completed-items-dropdown">
          <button
            className="complete-button waves-effect waves-light btn"
            onClick={() => setShowCompletedItems(!showCompletedItems)}
          >
            Completed
          </button>
          {showCompletedItems && (
            <ul>
              {completedItems.slice(0).map((item, index) => (
                <CompletedListItem
                  key={index}
                  item={item}
                  handleMoveBackToTask={handleMoveBackToTask}
                />
              ))}
            </ul>
          )}
        </div>
      )}
    </div>
  );
}

export default TaskList;
