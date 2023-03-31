import React, { useState, useEffect, useRef, useCallback } from "react";
import "./TaskListScreen.css";
import TaskItem from "./TaskItem/TaskItem";
import CompletedListItem from "./TaskItem/CompletedTaskItem";
import { fetchTasks, addTask, deleteTask, updateTask } from "../api/tasks";

function TaskListScreen() {
  const [items, setItems] = useState([]);
  const [completedItems, setCompletedItems] = useState([]);
  const [inputValue, setInputValue] = useState("");
  const [invalidInput, setInvalidInput] = useState(false);
  const [showCompletedItems, setShowCompletedItems] = useState(false);
  const editingItemIdRef = useRef(null);

  useEffect(() => {
    fetchTasks().then(({ uncompletedTasks, completedTasks }) => {
      setItems(uncompletedTasks);
      setCompletedItems(completedTasks);
    });
  }, []);

  const handleAddTaskItem = () => {
    if (inputValue.trim()) {
      addTask(inputValue).then((data) => setItems([data, ...items]));

      setInputValue("");
      setInvalidInput(false);
    } else {
      setInvalidInput(true);
    }
  };

  const handleRemoveItem = (id) => {
    deleteTask(id).then(() => {
      setItems(items.filter((item) => item.id !== id));
      setCompletedItems(completedItems.filter((item) => item.id !== id));
    });
  };

  const handleCompleteItem = (item) => {
    const updatedItem = { ...item, completed: true };
    updateTask(item.id, updatedItem).then(() => {
      setItems(items.filter((i) => i.id !== item.id));
      setCompletedItems([item, ...completedItems]);
    });
  };

  const handleMoveBackToTask = (item) => {
    const updatedItem = { ...item, completed: false };
    updateTask(item.id, updatedItem).then(() => {
      setCompletedItems(completedItems.filter((i) => i.id !== item.id));
      setItems([item, ...items]);
    });
  };

  const handleUpdateItem = useCallback(
    (itemId, newTitle) => {
      // Update the task title in the local state
      const updatedItems = items.map((item) => {
        if (item.id === itemId) {
          return { ...item, title: newTitle };
        }
        return item;
      });
      setItems(updatedItems);

      // Reset the editingItemId state
      editingItemIdRef.current = null;

      // PUT the updated task title to the API
      const updatedTask = {
        ...updatedItems.find((item) => item.id === itemId),
        title: newTitle,
      };
      updateTask(itemId, updatedTask);
    },
    [items]
  );

  const handleEditingTask = (id) => {
    if (editingItemIdRef.current !== null && editingItemIdRef.current !== id) {
      editingItemIdRef.current = id;
    }
  };

  const hasCompletedItems = completedItems.length > 0;

  return (
    <div className="container tasklist-container">
      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleAddTaskItem();
        }}
        data-testid="task-form"
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

      <ul testis="task-list">
        {items.reverse().map((item, index) => (
          <TaskItem
            key={index}
            item={item}
            index={index}
            handleRemoveItem={handleRemoveItem}
            handleCompleteItem={handleCompleteItem}
            handleUpdateItem={handleUpdateItem}
            editingItemIdRef={editingItemIdRef}
            handleEditingTask={handleEditingTask}
          />
        ))}
      </ul>

      {hasCompletedItems && (
        <div className="completed-items-dropdown">
          <button
            id="completed-button"
            className="complete-button waves-effect waves-light btn"
            onClick={() => setShowCompletedItems(!showCompletedItems)}
          >
            Completed
          </button>
          {showCompletedItems && (
            <ul data-testid="completed-task-list">
              {completedItems.slice(0).map((item, index) => (
                <CompletedListItem
                  key={index}
                  item={item}
                  completedItems={completedItems}
                  handleRemoveItem={handleRemoveItem}
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

export default TaskListScreen;
