<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="xml" indent="yes" />	
  <xsl:template match="/">
    <testExecutions version="1">
      <xsl:for-each-group select="//test-run//test-case/ancestor::test-suite[last()]" group-by="@name">
        <xsl:variable name="a" select="current-grouping-key()" />
        <file path="{$a}">
          <xsl:for-each select="//test-run/test-suite[@name=$a]//test-case">
            <xsl:variable name="caseName" select="current()/@fullname" />
            <xsl:variable name="caseDuration" select="round(number(current()/@duration) * 1000)" />
            <xsl:variable name="testResult" select="current()/@result" />
            <testCase name="{$caseName}" duration="{$caseDuration}">
              <xsl:choose>
                <xsl:when test="$testResult = 'Failed'">
                  <xsl:variable name="reason" select="current()/reason/message" />
                  <failure message="Failed: {$reason}"><xsl:value-of select="current()/output[last()]"/></failure>
                </xsl:when>
                <xsl:when test="$testResult = 'Ignored'">
                  <xsl:variable name="reason" select="current()/reason/message" />
                  <skipped message="Ignorred: {$reason}"><xsl:value-of select="current()/output[last()]"/></skipped>
                </xsl:when>
                <xsl:when test="$testResult = 'Warning'">
                  <xsl:variable name="reason" select="current()/reason/message" />
                  <error message="Warning: {$reason}"><xsl:value-of select="current()/output[last()]"/></error>
                </xsl:when>
                <xsl:when test="$testResult = 'Inconclusive'">
                  <xsl:variable name="reason" select="current()/reason/message" />
                  <error message="Inconclusive: {$reason}"><xsl:value-of select="current()/output[last()]"/></error>
                </xsl:when>
              </xsl:choose>
            </testCase>
          </xsl:for-each>
        </file>
      </xsl:for-each-group>
    </testExecutions>
  </xsl:template>

</xsl:stylesheet>