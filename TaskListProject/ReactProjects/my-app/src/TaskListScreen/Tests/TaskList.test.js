import React from "react";
import { render, screen } from "@testing-library/react";
import TaskList from "../TaskListScreen";

describe("TaskList", () => {
  it("should render the input label correctly", () => {
    render(<TaskList />);
    const inputLabel = screen.getByLabelText("Enter your task");
    expect(inputLabel).toBeInTheDocument();
  });
});
