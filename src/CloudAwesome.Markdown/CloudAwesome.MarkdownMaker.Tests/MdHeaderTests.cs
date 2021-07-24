﻿using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdHeaderTests
    {
        [Test]
        [TestCase("Page Title", 1, "# Page Title")]
        [TestCase("The top of the page", 1, "# The top of the page")]
        [TestCase("Secondary header", 2, "## Secondary header")]
        [TestCase("Way down the list header", 6, "###### Way down the list header")]
        public void Header_Returns_Valid_Markdown(string text, int level, string expectedResult)
        {
            var header = new MdHeader(text, level);
            var actualResult = header.Markdown;
            
            actualResult.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("Way down the list header", 7)]
        [TestCase("Way too far down the list of headers", 10)]
        public void Level_7_Header_Throws_Validation_Exception(string text, int level)
        {
            var header = new MdHeader(text, level);
            Func<string> function = () => header.Markdown;

            function.Should().Throw<InputValidationException>();
        }

        [Test]
        [TestCase("Start here", 8, "Updated here", 4, "#### Updated here")]
        public void Inputs_Can_Updated_After_Construction(string text, int level,
            string changedText, int changedLevel, string expectedResult)
        {
            var header = new MdHeader(text, level);
            header.Text = changedText;
            header.Level = changedLevel;

            var actualResult = header.Markdown;

            actualResult.Should().Be(expectedResult);
        }
    }
}