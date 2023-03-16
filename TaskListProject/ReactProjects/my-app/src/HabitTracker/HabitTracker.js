import React, { useState } from "react";
import "./HabitTracker.css";
import ListItem from "./ListItem";
import CompletedListItem from "./CompletedListItem";

function HabitTracker() {
  const [items, setItems] = useState([]);
  const [completedItems, setCompletedItems] = useState([]);
  const [inputValue, setInputValue] = useState("");
  const [invalidInput, setInvalidInput] = useState(false);
  const [showCompletedItems, setShowCompletedItems] = useState(false);

  const handleAddItem = () => {
    if (inputValue.trim()) {
      setItems([inputValue, ...items]);
      setInputValue("");
      setInvalidInput(false);
    } else {
      setInvalidInput(true);
    }
  };

  const handleRemoveItem = (index) => {
    setItems(items.filter((item, i) => i !== index));
  };

  const handleCompleteItem = (item, index) => {
    setItems(items.filter((i, idx) => idx !== index));
    const currentTime = new Date().toLocaleString();
    setCompletedItems([{ item, time: currentTime }, ...completedItems]);
  };

  const handleMoveBackToTask = (item) => {
    setCompletedItems(completedItems.filter((i) => i !== item));
    setItems([item.item, ...items]);
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

export default HabitTracker;
