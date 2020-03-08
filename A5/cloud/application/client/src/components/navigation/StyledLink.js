import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

/**
 * Styled component to avoid custom css for each react router link
 */
const StyledLink = styled(Link)`
  text-decoration: none;

  &:focus,
  &:hover,
  &:visited,
  &:link,
  &:active {
    text-decoration: none;
    color: #9f9f9f;
  }
`;

export default props => <StyledLink {...props} />;
