import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import "./App.css";
import TaskList from "./TaskList/TaskList.js";

function App() {
  return (
    <Router>
      <div className="App">
        <TaskList listId={1} />
      </div>
    </Router>
  );
}

export default App;
