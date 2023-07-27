import React from "react";
import { render, fireEvent, screen } from "@testing-library/react";
import Container from "./Container";

describe("Container Component Tests", () => {
  it("renders the component without errors", () => {
    render(<Container />);
  });

  it("displays the 'Small' container when clicked", () => {
    render(<Container />);
    const smallContainer = screen.getByText("Small");
    fireEvent.click(smallContainer);
    expect(smallContainer).toHaveClass("selected-container");
  });

  it("displays the 'Medium' container when clicked", () => {
    render(<Container />);
    const mediumContainer = screen.getByText("Medium");
    fireEvent.click(mediumContainer);
    expect(mediumContainer).toHaveClass("selected-container");
  });

  it("displays the 'Large' container when clicked", () => {
    render(<Container />);
    const largeContainer = screen.getByText("Large");
    fireEvent.click(largeContainer);
    expect(largeContainer).toHaveClass("selected-container");
  });

  it("displays the 'Custom' container when clicked", () => {
    render(<Container />);
    const customContainer = screen.getByText("Custom");
    fireEvent.click(customContainer);
    expect(customContainer).toHaveClass("selected-container");
  });

  it("renders the 'Continue' button disabled initially", () => {
    render(<Container />);
    const continueButton = screen.getByText("Continue");
    expect(continueButton).toBeDisabled();
  });

  it("enables the 'Continue' button after selecting a container", () => {
    render(<Container />);
    const smallContainer = screen.getByText("Small");
    const continueButton = screen.getByText("Continue");

    fireEvent.click(smallContainer);
    expect(continueButton).toBeEnabled();
  });

  it("enables the 'Continue' button after filling the custom container details", () => {
    render(<Container />);
    const customContainer = screen.getByText("Custom");
    const continueButton = screen.getByText("Continue");

    fireEvent.click(customContainer);

    const heightInput = screen.getByPlaceholderText("Enter height");
    const widthInput = screen.getByPlaceholderText("Enter width");
    const lengthInput = screen.getByPlaceholderText("Enter length");

    fireEvent.change(heightInput, { target: { value: "500" } });
    fireEvent.change(widthInput, { target: { value: "600" } });
    fireEvent.change(lengthInput, { target: { value: "700" } });

    expect(continueButton).toBeEnabled();
  });
});
